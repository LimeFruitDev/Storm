using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using Sandbox;
using Sandbox.Diagnostics;

namespace Storm;

public static class ApiService
{
	private static ApiConfig _config;
	private static WebSocket _socket;
	private static ulong _currentMessageId;
	private static readonly Dictionary<ulong, Action<ulong, string>> _callbacks = new();
	public static Logger Log = new("ApiService");

	public static ApiStatus Status { get; private set; } = ApiStatus.Uninitialized;

	public static async void Initialize()
	{
		Game.AssertServer();
		ThreadSafe.AssertIsMainThread();

		if (Status == ApiStatus.Connected)
		{
			Log.Warning("ApiService::Initialize was called while the service is already live!");
			return;
		}

		_config = LoadConfig();
		if (_config == null)
		{
			Status = ApiStatus.BadConfig;
			return;
		}

		try
		{
			_socket = new WebSocket(_config.MaxMessageSize);
			await _socket.Connect($"{(_config.Encrypted ? "wss" : "ws")}://{_config.Host}:{_config.Port}/");

			_socket.OnDataReceived += RawDataReceivedHandler;
			_socket.OnMessageReceived += MessageReceivedHandler;
			_socket.OnDisconnected += DisconnectHandler;

			Status = ApiStatus.Connected;
			Log.Info("A WebSocket connection has been established successfully.");
		}
		catch (Exception ex)
		{
			_socket = null;
			Status = ApiStatus.Failed;
			Log.Error(ex, "Failed to establish a WebSocket connection.");
		}
	}

	public static void Disconnect()
	{
		if (Status != ApiStatus.Connected || _socket == null)
			return;

		Log.Info("Disconnecting!");

		_socket.Dispose();
		_socket = null;
		Status = ApiStatus.Disconnected;
	}

#pragma warning disable CA2012
	public static void SendMessage<T>(T message) where T : BaseRpc
	{
		lock (_socket)
		{
			_socket.Send(JsonSerializer.Serialize(message));
		}
	}

	public static void SendMessage<T>(T message, Action<ulong, string> callback) where T : BaseRpc
	{
		_callbacks[message.UniqueId] = callback;

		lock (_socket)
		{
			_socket.Send(JsonSerializer.Serialize(message));
		}
	}
#pragma warning restore CA2012

	public static ulong GetNextMessageId()
	{
		return _currentMessageId++;
	}

	private static ApiConfig LoadConfig()
	{
		throw new NotImplementedException();
	}

	private static void RawDataReceivedHandler(Span<byte> data)
	{
		Log.Warning($"Received {data.Length} bytes of raw data from the connection, not handling!");
	}

	private static async void MessageReceivedHandler(string message)
	{
		var rootElement = await Utils.ParseJson(message);

		var uniqueId = rootElement.GetProperty("uniqueId").GetUInt64();
		if (!_callbacks.ContainsKey(uniqueId)) return;

		_callbacks[uniqueId].Invoke(uniqueId, message);
		_callbacks.Remove(uniqueId);
	}

	private static void DisconnectHandler(int status, string reason)
	{
		Log.Warning($"The connection to the remote host has ended: {reason} ({status})!");

		if (status != 1000 && Status != ApiStatus.Disconnected)
			Log.Error("The connection has closed abnormally!");
	}
}
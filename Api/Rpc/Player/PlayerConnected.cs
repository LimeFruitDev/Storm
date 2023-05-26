﻿using System.Text.Json.Serialization;

namespace Storm;

public class PlayerConnectedRpc : BaseRpc
{
	public PlayerConnectedRpc(Player player)
	{
		MessageType = "playerConnected";
		SteamName = player.SteamName;
		SteamId = player.SteamId;
	}

	[JsonPropertyName("steamName")] public string SteamName { get; set; }
	[JsonPropertyName("steamId")] public string SteamId { get; set; }
}

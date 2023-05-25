using System.Text.Json.Serialization;

namespace Storm;

public class BaseRpc
{
	public BaseRpc()
	{
		UniqueId = ApiService.GetNextMessageId();
		MessageType = "base_rpc";
	}

	[JsonPropertyName("uniqueId")] public ulong UniqueId { get; set; }
	[JsonPropertyName("type")] public string MessageType { get; set; }
}
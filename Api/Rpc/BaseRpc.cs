using System.Text.Json.Serialization;

namespace Storm;

public class BaseRpc
{
	public BaseRpc()
	{
		UniqueId = ApiService.GetNextMessageId();
		MessageType = "baseRpc";
	}

	[JsonPropertyName("uniqueId")] public ulong UniqueId { get; set; }
	[JsonPropertyName("type")] public string MessageType { get; set; }
}

using System.Text.Json.Serialization;

namespace Storm;

public class PlayerConnectedResponse : BaseResponse
{
	[JsonPropertyName("playerId")] public string PlayerId { get; set; }
	[JsonPropertyName("whitelist")] public string Whitelist { get; set; }
	[JsonPropertyName("blacklist")] public string Blacklist { get; set; }
	[JsonPropertyName("characters")] public string Characters { get; set; }
}

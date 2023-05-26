using System.Text.Json;
using System.Text.Json.Serialization;

namespace Storm;

public class SavePlayerRpc : BaseRpc
{
	public SavePlayerRpc(Player player)
	{
		MessageType = "savePlayer";
		Data = JsonSerializer.Serialize(player.Data);
		SteamName = player.SteamName;
		Whitelist = JsonSerializer.Serialize(player.Whitelist);
		Blacklist = JsonSerializer.Serialize(player.Blacklist);
	}

	[JsonPropertyName("data")] public string Data { get; set; }
	[JsonPropertyName("steamName")] public string SteamName { get; set; }
	[JsonPropertyName("whitelist")] public string Whitelist { get; set; }
	[JsonPropertyName("blacklist")] public string Blacklist { get; set; }
}

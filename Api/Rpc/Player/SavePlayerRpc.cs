using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Storm;

public class SavePlayerRpc : BaseRpc
{
	public SavePlayerRpc(Player player)
	{
		MessageType = "savePlayer";
		PlayerId = player.UniqueId;
		SteamName = player.SteamName;
		SteamId = player.SteamId;
		Whitelist = JsonSerializer.Serialize(player.Whitelist);
		Blacklist = JsonSerializer.Serialize(player.Blacklist);
		Data = JsonSerializer.Serialize(player.Data);
	}

	[JsonPropertyName("playerId")] public Guid PlayerId { get; set; }
	[JsonPropertyName("steamId")] public string SteamId { get; set; }
	[JsonPropertyName("steamName")] public string SteamName { get; set; }
	[JsonPropertyName("whitelist")] public string Whitelist { get; set; }
	[JsonPropertyName("blacklist")] public string Blacklist { get; set; }
	[JsonPropertyName("data")] public string Data { get; set; }
}

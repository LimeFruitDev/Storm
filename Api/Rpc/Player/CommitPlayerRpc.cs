using System;
using System.Text.Json.Serialization;

namespace Storm;

public class CommitPlayerRpc : BaseRpc
{
	public CommitPlayerRpc(Player player)
	{
		MessageType = "player_commit";
		PlayerId = player.UniqueId;
		SteamName = player.SteamName;
		SteamId = player.SteamId;
	}

	[JsonPropertyName("playerId")] public Guid PlayerId { get; set; }
	[JsonPropertyName("steamName")] public string SteamName { get; set; }
	[JsonPropertyName("steamId")] public string SteamId { get; set; }
}
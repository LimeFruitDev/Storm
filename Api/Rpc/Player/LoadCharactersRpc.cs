using System;
using System.Text.Json.Serialization;

namespace Storm;

public class LoadCharactersRpc : BaseRpc
{
	public LoadCharactersRpc(Player player)
	{
		MessageType = "loadCharacters";
		PlayerId = player.UniqueId;
	}

	[JsonPropertyName("playerId")] public Guid PlayerId { get; set; }
}
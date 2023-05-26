using System;
using System.Text.Json.Serialization;

namespace Storm;

public class CommitCharacterRpc : BaseRpc
{
	public CommitCharacterRpc(Character character)
	{
		MessageType = "commitCharacter";
		CharacterId = character.UniqueId;
		PlayerId = character.Player.UniqueId;
		Name = character.Name;
		Model = character.Model;
		Faction = character.Faction;
	}

	[JsonPropertyName("characterId")] public Guid CharacterId { get; set; }
	[JsonPropertyName("playerId")] public Guid PlayerId { get; set; }
	[JsonPropertyName("name")] public string Name { get; set; }
	[JsonPropertyName("model")] public string Model { get; set; }
	[JsonPropertyName("faction")] public string Faction { get; set; }
}
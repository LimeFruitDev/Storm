using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Storm;

public class SaveCharacterRpc : BaseRpc
{
	public SaveCharacterRpc(Character character)
	{
		MessageType = "saveCharacter";
		CharacterId = character.UniqueId;
		Name = character.Name;
		Model = character.Model;
		Faction = character.Faction;
		Data = JsonSerializer.Serialize(character.Data);
	}

	[JsonPropertyName("characterId")] public Guid CharacterId { get; set; }
	[JsonPropertyName("name")] public string Name { get; set; }
	[JsonPropertyName("model")] public string Model { get; set; }
	[JsonPropertyName("faction")] public string Faction { get; set; }
	[JsonPropertyName("data")] public string Data { get; set; }
}
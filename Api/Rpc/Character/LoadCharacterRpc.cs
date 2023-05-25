using System;
using System.Text.Json.Serialization;

namespace Storm;

public class LoadCharacterRpc : BaseRpc
{
	public LoadCharacterRpc(Character character)
	{
		MessageType = "character_load";
		CharacterId = character.UniqueId;
	}

	[JsonPropertyName("characterId")] public Guid CharacterId { get; set; }
}
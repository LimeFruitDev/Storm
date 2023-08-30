
using System;
using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace Storm;

public partial class Player
{
	[Net] public Character Character { get; set; }
	[Net] public IList<Character> Characters { get; set; }

	public bool SelectCharacter(Guid characterId)
	{
		var character = Characters.FirstOrDefault(c => c.UniqueId.Equals(characterId));
		if (character == null)
			return false;

		// TODO: Display error messages accordingly
		if (!IsAlive || !HasWhitelist(character.Faction))
			return false;

		// TODO: Implement char swapping
		// TODO: Run pre, post events?
		// TODO: Run events to see if the player should be able to load the character
		Character = character;
		SetModel(character.Model);
		Spawn();

		// todo: position saving?

		return true;
	}
}

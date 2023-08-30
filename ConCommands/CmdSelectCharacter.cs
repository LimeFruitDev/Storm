using System;
using Sandbox;

namespace Storm;

public static partial class ConCommands
{
	[ConCmd.Server("storm.character.select")]
	public static void CmdSelectCharacter(Guid characterId)
	{
		if (ConsoleSystem.Caller is null)
		{
			Log.Error("You can't select a character from the console!");
			return;
		}

		if (ConsoleSystem.Caller?.Pawn is not Player player)
		{
			Log.Error(
				$"Tried to select a character before a player was successfully set up. (Client: {ConsoleSystem.Caller!.SteamId})");
			return;
		}

		// TODO: Check value, get angry at the player if necessary with a very angry message :)
		player.SelectCharacter(characterId);
	}
}
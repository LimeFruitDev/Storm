using System;
using Sandbox;

namespace Storm;

public static partial class ConCommands
{
	[ConCmd.Server("storm.character.create")]
	public static void CmdCreateCharacter(string name, string model, string faction, bool switchTo)
	{
		if (ConsoleSystem.Caller is null)
		{
			Log.Error("You can't create a character from the console!");
			return;
		}

		if (ConsoleSystem.Caller?.Pawn is not Player player)
		{
			Log.Error(
				$"Tried to create a character before a player was successfully set up. (Client: {ConsoleSystem.Caller!.SteamId})");
			return;
		}

		if (!FactionManager.Instance.Factions.TryGetValue(faction, out var factionData))
		{
			Log.Error(
				$"Tried to create a character on invalid faction \"{faction}\" for player {player.VerboseName}");
			return;
		}

		if (!factionData.Models.Contains(model))
		{
			Log.Error(
				$"Player {player.VerboseName} tried to create a character in the \"{faction}\" faction with unsupported model \"{model}\"!");
			return;
		}

		var character = new Character
		{
			UniqueId = Guid.NewGuid(),
			Player = player,
			Name = name,
			Model = model,
			Faction = faction
		};

		player.Characters.Add(character);
		character.PersistentObject.MarkDirty();

		if (switchTo)
			player.SelectCharacter(character.UniqueId);
	}
}
using System.Collections.Generic;
using Sandbox;

namespace Storm;

public partial class Player
{
	[Net] public IList<string> Whitelist { get; set; } // To grant access to private factions
	[Net] public IList<string> Blacklist { get; set; } // To take access from public factions

	public bool HasWhitelist(string faction, bool real = false)
	{
		if (!real && FactionManager.Instance.Factions[faction].IsDefault)
			return !IsBlacklisted(faction);

		return Whitelist.Contains(faction);
	}

	public void AddWhitelist(string faction)
	{
		if (HasWhitelist(faction))
		{
			Log.Info($"Not adding {VerboseName} to the {faction} faction whitelist because they are already listed.");
			return;
		}

		if (FactionManager.Instance.Factions[faction].IsDefault)
		{
			Log.Info(
				$"Not adding {VerboseName} to the {faction} faction whitelist because the faction is accessible to everyone.");
			return;
		}

		Whitelist.Add(faction);
		Log.Info($"Added {VerboseName} to the {faction} faction whitelist.");
	}

	public void RemoveWhitelist(string faction)
	{
		if (!HasWhitelist(faction))
		{
			Log.Info($"Not removing {VerboseName} from the {faction} faction whitelist because they aren't listed.");
			return;
		}

		if (FactionManager.Instance.Factions[faction].IsDefault)
		{
			Log.Info(
				$"Not removing {VerboseName} from the {faction} faction whitelist because the faction is accessible to everyone (use blacklists).");
			return;
		}

		Whitelist.Remove(faction);
		Log.Info($"Removed {VerboseName} from the {faction} faction whitelist.");
	}

	public bool IsBlacklisted(string faction)
	{
		return Blacklist.Contains(faction);
	}

	public void AddBlacklist(string faction)
	{
		if (Blacklist.Contains(faction))
		{
			Log.Info($"Not adding {VerboseName} to the {faction} faction blacklist because they are already listed.");
			return;
		}

		if (!FactionManager.Instance.Factions[faction].IsDefault)
		{
			Log.Info(
				$"Not adding {VerboseName} to the {faction} faction blacklist because the faction isn't default, remove their whitelist instead!");
			return;
		}

		Blacklist.Add(faction);
		Log.Info($"Added {VerboseName} to the {faction} faction blacklist.");
	}

	public void RemoveBlacklist(string faction)
	{
		if (!Blacklist.Contains(faction))
		{
			Log.Info($"Not removing {VerboseName} from the {faction} faction blacklist because they aren't listed.");
			return;
		}

		if (!FactionManager.Instance.Factions[faction].IsDefault)
		{
			Log.Info(
				$"Not removing {VerboseName} from the {faction} faction blacklist because the faction isn't default, whitelist them instead!");
			return;
		}

		Blacklist.Remove(faction);
		Log.Info($"Removed {VerboseName} from the {faction} faction blacklist.");
	}
}
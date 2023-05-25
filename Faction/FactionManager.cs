using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.Diagnostics;

namespace Storm;

public partial class FactionManager : Entity
{
	public FactionManager()
	{
		Transmit = TransmitType.Always;
		Tags.Add("system");
	}

	public static FactionManager Instance { get; private set; }
	public static Logger Log { get; } = new("FactionManager");

	[Net] public IDictionary<string, BaseFaction> Factions { get; set; }

	[GameEvent.Initialize.Server]
	public static void Initialize()
	{
		if (Instance?.IsValid() ?? false)
			return;

		Instance = Game.IsServer ? new FactionManager() : All.OfType<FactionManager>().First();
		if (!Instance.IsValid())
			Log.Error("Initialization failed!");
	}

	public override void Spawn()
	{
		Log.Info("FactionManager is spawning in this instance.");
		base.Spawn();

		if (!Game.IsServer)
		{
			Instance ??= this;
			return;
		}

		Factions = new Dictionary<string, BaseFaction>();
		foreach (var factionTypeDescription in TypeLibrary.GetTypes<BaseFaction>())
		{
			if (factionTypeDescription.Name == "BaseFaction")
				continue;

			var faction = factionTypeDescription.Create<BaseFaction>();
			if (faction == null)
			{
				Log.Warning("Failed to instantiate faction instance!");
				continue;
			}

			Factions.Add(faction.UniqueId, faction);
			Log.Info($"Registered faction {faction.Name} ({faction.UniqueId}).");
		}
	}

	public override void ClientSpawn()
	{
		Initialize();
		base.ClientSpawn();
	}
}
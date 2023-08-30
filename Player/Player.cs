using System;
using System.Collections;
using System.Collections.Generic;
using Sandbox;

namespace Storm;

// todo: deaths, respawns, corpses, etc
// todo: spawning & setup
// todo: animations, footsteps & such
// todo: taking damage
public partial class Player : AnimatedEntity
{
	public Player()
	{
		PersistentObject = new PersistentObject<Player, SavePlayerRpc>(this);
		Characters = new List<Character>();
	}

	public PersistentObject<Player, SavePlayerRpc> PersistentObject { init; get; }
	[Net] public Guid UniqueId { get; set; }

	public string SteamName => Client.Name;
	public string SteamId => Client.SteamId.ToString();
	public bool IsAlive => LifeState == LifeState.Alive;
	public string VerboseName => $"{SteamName}({SteamId})<{UniqueId}>";
}
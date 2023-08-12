using System;
using System.Collections.Generic;
using Sandbox;

namespace Storm;

public partial class Character : EntityComponent
{
	public Character()
	{
		PersistentObject = new PersistentObject<Character, SaveCharacterRpc>(this);
		Data = new Dictionary<string, object>();
	}

	public PersistentObject<Character, SaveCharacterRpc> PersistentObject { init; get; }
	[Net] public Guid UniqueId { get; set; }
	[Net] public Player Player { get; set; }
	[Net] public new string Name { get; set; }
	[Net] public string Model { get; set; }
	[Net] public string Faction { get; set; }
	[Net] public IDictionary<string, object> Data { get; set; }

	public BaseFaction FactionData => FactionManager.Instance.Factions[Faction];

	public void SetData(string key, object value)
	{
		Game.AssertServer();
		Data[key] = value;
	}

	public object GetData(string key, object @default = null)
	{
		return Data.TryGetValue(key, out var value) ? value : @default;
	}

	public T GetData<T>(string key, T @default = null) where T : class
	{
		return Data.TryGetValue(key, out var value) ? (T)value : @default;
	}
}
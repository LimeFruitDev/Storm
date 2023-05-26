using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.Diagnostics;

namespace Storm;

public partial class ItemManager : Entity
{
	public static readonly Guid WorldInventoryId = Guid.Parse("e5254f5e-66f5-451f-8848-42d3235e28b9");

	public ItemManager()
	{
		Transmit = TransmitType.Always;
		Tags.Add("system");
	}

	public static ItemManager Instance { get; private set; }
	public static Logger Log { get; } = new("ItemManager");

	[Net] public IDictionary<Guid, BaseInventory> Inventories { get; set; }
	[Net] public IDictionary<string, ItemData> ItemDatabase { get; set; }
	[Net] public IDictionary<Guid, ItemInstance> ItemInstances { get; set; }

	[GameEvent.Initialize.Server]
	public static void Initialize()
	{
		if (Instance?.IsValid() ?? false)
			return;

		Instance = Game.IsServer ? new ItemManager() : All.OfType<ItemManager>().First();
		if (!Instance.IsValid())
			Log.Error("Initialization failed!");
	}

	public override void Spawn()
	{
		Log.Info("ItemManager is spawning in this instance.");
		base.Spawn();

		if (!Game.IsServer)
		{
			Instance ??= this;
			return;
		}

		Inventories = new Dictionary<Guid, BaseInventory>();
		ItemDatabase = new Dictionary<string, ItemData>();
		ItemInstances = new Dictionary<Guid, ItemInstance>();
	}

	public override void ClientSpawn()
	{
		Initialize();
		base.ClientSpawn();
	}

	public ItemInstance CreateItem(string type)
	{
		var uniqueId = Guid.NewGuid();

		ItemInstance itemInstance = new()
		{
			UniqueId = uniqueId,
			Data = (ItemData)ItemDatabase[type].Clone(),
			InventoryId = WorldInventoryId
		};

		Log.Info($"Created item {type}({uniqueId}).");
		ItemInstances.Add(uniqueId, itemInstance);
		return itemInstance;
	}

	public bool DeleteItem(ItemInstance item)
	{
		if (!ItemInstances.ContainsKey(item.UniqueId))
			return false;

		// todo: send delete RPC, log
		ItemInstances.Remove(item.UniqueId);
		return true;
	}
}
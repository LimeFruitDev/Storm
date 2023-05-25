using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace Storm;

public partial class LinearInventory : BaseInventory
{
	public override string Type => "base_linear";

	[Net] public IList<ItemInstance> Items { get; set; }

	public override IEnumerable<ItemInstance> GetItemsOfType(string type)
	{
		return Items.Where(item => item.Type == type);
	}
	
	public override IEnumerable<ItemInstance> FilterItems(Func<ItemInstance, bool> filter)
	{
		return Items.Where(filter);
	}
	
	public override ItemInstance HasItem(string type)
	{
		return GetItemsOfType(type).FirstOrDefault();
	}
	
	public override ItemInstance HasItem(ItemInstance item)
	{
		return FilterItems(inventoryItem => inventoryItem.UniqueId == item.UniqueId).FirstOrDefault();
	}

	public override ItemInstance AddItem(string type)
	{
		var item = ItemManager.Instance.CreateItem(type);
		Items.Add(item);
		return item;
	}

	public override bool AddItem(ItemInstance item)
	{
		if (item.InventoryId != ItemManager.WorldInventoryId)
		{
			var inventory = ItemManager.Instance.Inventories[item.InventoryId];
			if (!inventory.RemoveItem(item))
			{
				ItemManager.Log.Warning(
					$"Failed to add item {item.UniqueId} into inventory {UniqueId} because it could not be removed from inventory {inventory.UniqueId}!");
				return false;
			}
		}

		Items.Add(item);
		item.InventoryId = UniqueId;
		return true;
	}

	public override bool RemoveItem(string type, out ItemInstance item)
	{
		var itemInstance = HasItem(type);
		if (!RemoveItem(itemInstance))
		{
			item = null;
			return false;
		}

		item = itemInstance;
		return true;
	}

	public override bool RemoveItem(ItemInstance item)
	{
		if (item.InventoryId != UniqueId)
			return false;

		// BUG: If we fail to remove the item from the Items list then it's perpetually stuck in 'World' until the server restarts.
		// TODO: We should replace this with a sort of 'transfer' operation so we can do something about creating an entity and such?
		item.InventoryId = ItemManager.WorldInventoryId;
		return Items.Remove(item);
	}
}
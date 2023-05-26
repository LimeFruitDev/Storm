
using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace Storm;

public partial class GridInventory : BaseInventory
{
	public override string Type => "base_grid";

	[Net] public IDictionary<Vector2, ItemInstance> Items { get; set; }
	[Net] public int Width { get; set; }
	[Net] public int Height { get; set; }

	public override IEnumerable<ItemInstance> GetItemsOfType(string type)
	{
		return Items.Values.ToList().Where(item => item.Type == type);
	}

	public override IEnumerable<ItemInstance> FilterItems(Func<ItemInstance, bool> filter)
	{
		return Items.Values.ToList().Where(filter);
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
		throw new NotImplementedException();
	}

	public override bool AddItem(ItemInstance item)
	{
		throw new NotImplementedException();
	}

	public override bool RemoveItem(ItemInstance item)
	{
		throw new NotImplementedException();
	}
}

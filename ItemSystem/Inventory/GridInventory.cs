
using System;
using System.Collections.Generic;
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
		throw new NotImplementedException();
	}

	public override IEnumerable<ItemInstance> FilterItems(Func<ItemInstance, bool> filter)
	{
		throw new NotImplementedException();
	}

	public override ItemInstance HasItem(string type)
	{
		throw new NotImplementedException();
	}

	public override ItemInstance HasItem(ItemInstance item)
	{
		throw new NotImplementedException();
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

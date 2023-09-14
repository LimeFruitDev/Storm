
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sandbox;

namespace Storm;

public partial class GridInventory : BaseInventory
{
	public override string Type => "base_grid";

	[Net] public IDictionary<Point, ItemInstance> Items { get; set; }
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
		var point = FindSpaceForItem(item);

		if (point is null)
		{
			item.Rotated = !item.Rotated;
			point = FindSpaceForItem(item);

			if (point is null)
			{
				item.Rotated = !item.Rotated;
				return false;
			}
		}

		Items[point.Value] = item;
		return true;
	}

	public override bool RemoveItem(ItemInstance item)
	{
		throw new NotImplementedException();
	}

	private bool CheckItemFitsInPosition(Point position, ItemInstance item)
	{
		if (position.X + item.Width > Width || position.Y + item.Height > Height)
			return false;

		// TODO: Check the efficiency of this.
		// At some point we should probably move this to just modifying "position" since it's passed by value anyway.
		for (var j = position.Y; j < position.Y + item.Height; ++j)
		{
			for (var i = position.X; i < position.X + item.Width; ++i)
			{
				if (Items[new Point(i, j)] is not null)
					return false;
			}
		}

		return true;
	}

	private Point? FindSpaceForItem(ItemInstance item)
	{
		for (var i = 0; i < Width - item.Width + 1; ++i)
		{
			for (var j = 0; j < Height - item.Height + 1; ++j)
			{
				var point = new Point(i, j);
				if (CheckItemFitsInPosition(new Point(i, j), item))
					return point;
			}
		}

		return null;
	}
}

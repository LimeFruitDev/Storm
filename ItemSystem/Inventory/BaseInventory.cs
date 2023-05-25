using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace Storm;

public abstract partial class BaseInventory : BaseNetworkable
{
	/// <summary>
	///     The type of the inventory in question, such as player, container, etc.
	/// </summary>
	public virtual string Type => "base";

	/// <summary>
	///     The unique identifier of the inventory in the database.
	/// </summary>
	[Net] public Guid UniqueId { get; set; }

	/// <summary>
	///     Returns the list of items contained within this inventory of the given type.
	/// </summary>
	/// <param name="type">The type of the item.</param>
	/// <returns>The items contained in the inventory with of given type.</returns>
	public abstract IEnumerable<ItemInstance> GetItemsOfType(string type);

	/// <summary>
	///     Returns the list of items contained within this inventory which pass the given filter.
	/// </summary>
	/// <param name="filter">The filter that defines which items will be returned.</param>
	/// <returns>The items contained in the inventory which pass the given filter.</returns>
	public abstract IEnumerable<ItemInstance> FilterItems(Func<ItemInstance, bool> filter);

	/// <summary>
	///     Checks whether or not the inventory contains an item of the given type.
	/// </summary>
	/// <param name="type">The type to look for.</param>
	/// <returns>The item if found, otherwise null.</returns>
	public abstract ItemInstance HasItem(string type);

	/// <summary>
	///     Checks whether or not the inventory contains an item.
	/// </summary>
	/// <param name="item">The item to look for.</param>
	/// <returns>The item if found, otherwise null</returns>
	public abstract ItemInstance HasItem(ItemInstance item);

	/// <summary>
	///     Adds an item of the given type to the inventory.
	/// </summary>
	/// <param name="type">The item type to add to the inventory.</param>
	/// <returns>The created instance.</returns>
	public abstract ItemInstance AddItem(string type);

	/// <summary>
	///     Adds an item to the inventory.
	/// </summary>
	/// <param name="item">The item to add.</param>
	/// <returns>The created instance.</returns>
	public abstract bool AddItem(ItemInstance item);

	/// <summary>
	///     Removes an item from the inventory.
	/// </summary>
	/// <param name="type">The type of the item to remove</param>
	/// <param name="item">The removed item instance.</param>
	/// <returns>True if the item was successfully removed, false otherwise.</returns>
	public virtual bool RemoveItem(string type, out ItemInstance item)
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

	/// <summary>
	///     Removes an item from the inventory.
	/// </summary>
	/// <param name="item">The item to remove.</param>
	/// <returns>True if the item was successfully removed, false otherwise.</returns>
	public abstract bool RemoveItem(ItemInstance item);

	/// <summary>
	///     Deletes an item from the inventory.
	/// </summary>
	/// <param name="type">The type of the item to delete.</param>
	/// <returns>True if the item was successfully deleted, false otherwise.</returns>
	public virtual bool DeleteItem(string type)
	{
		return RemoveItem(type, out var item) && DeleteItem(item);
	}

	/// <summary>
	///     Deletes an item from the inventory.
	/// </summary>
	/// <param name="item">The item to delete.</param>
	/// <returns>True if the item was successfully deleted, false otherwise.</returns>
	public virtual bool DeleteItem(ItemInstance item)
	{
		return RemoveItem(item) && ItemManager.Instance.DeleteItem(item);
	}
}
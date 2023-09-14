using System;
using Sandbox;

namespace Storm;

// TODO: Add PersistentObject
public partial class ItemInstance : BaseNetworkable
{
	public ItemInstance()
	{
		PersistentObject = new PersistentObject<ItemInstance, SaveItemRpc>(this);
	}

	public PersistentObject<ItemInstance, SaveItemRpc> PersistentObject { init; get; }
	[Net] public Guid UniqueId { get; set; }
	[Net] public ItemData Data { get; set; }
	[Net] public Guid InventoryId { get; set; }
	[Net] public ItemEntity Entity { get; set; }

	public bool Rotated
	{
		get => (bool)Data.GetProperty("rotated", false);
		set => Data.SetProperty("rotated", value);
	}

	public string Type => Data.UniqueId;
	public string Name => Data.Name;
	public string Description => Data.Description;
	public string WorldModel => Data.WorldModel;
	public int Weight => Data.Weight;
	public int Width => Rotated ? Data.Height : Data.Width;
	public int Height => Rotated ? Data.Width : Data.Height;
}
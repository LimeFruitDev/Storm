using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Storm;

public class SaveItemRpc : BaseRpc
{
	public SaveItemRpc(ItemInstance item)
	{
		MessageType = "saveItem";
		ItemId = item.UniqueId;
		InventoryId = item.InventoryId;
		Properties = JsonSerializer.Serialize(item.Data.Properties);
	}

	[JsonPropertyName("itemId")] public Guid ItemId { get; set; }
	[JsonPropertyName("inventoryId")] public Guid InventoryId { get; set; }
	[JsonPropertyName("properties")] public string Properties { get; set; }
}
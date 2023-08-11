using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Storm;

public class SaveItemRpc : BaseRpc
{
	public SaveItemRpc(ItemInstance item)
	{
		MessageType = "saveItem";
		UniqueId = item.UniqueId;
		InventoryId = item.InventoryId;
		Properties = JsonSerializer.Serialize(item.Data.Properties);
	}

	[JsonPropertyName("uniqueId")] public Guid UniqueId { get; set; }
	[JsonPropertyName("inventoryId")] public Guid InventoryId { get; set; }
	[JsonPropertyName("properties")] public string Properties { get; set; }
}
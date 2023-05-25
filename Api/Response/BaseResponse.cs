
using System.Text.Json.Serialization;

namespace Storm;

public class BaseResponse
{
	[JsonPropertyName("uniqueId")] public ulong UniqueId { get; set; }
}

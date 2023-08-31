using System.Text.Json.Serialization;

namespace Storm;

public class ApiConfig
{
	[JsonPropertyName("host")] public string Host { get; set; }
	[JsonPropertyName("port")] public int Port { get; set; }

	[JsonPropertyName("isEncrypted")] public bool Encrypted { get; set; }
	[JsonPropertyName("encryptionKey")] public string Key { get; set; }

	[JsonPropertyName("maxMessageSize")] public int MaxMessageSize { get; set; }
}
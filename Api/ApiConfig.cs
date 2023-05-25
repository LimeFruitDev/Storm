namespace Storm;

public class ApiConfig
{
	public string Host { get; set; }
	public int Port { get; set; }

	public bool Encrypted { get; set; }
	public string Key { get; set; }

	public int MaxMessageSize { get; set; }
}
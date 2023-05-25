using System.Collections.Generic;
using Sandbox;

namespace Storm;

public partial class Player
{
	[Net] public IDictionary<string, object> Data { get; set; }

	public void SetData(string key, object value)
	{
		Data[key] = value;
	}

	public object GetData(string key, object @default = null)
	{
		return Data.TryGetValue(key, out var value) ? value : @default;
	}

	public T GetData<T>(string key, T @default = null) where T : class
	{
		return Data.TryGetValue(key, out var value) ? (T)value : @default;
	}
}
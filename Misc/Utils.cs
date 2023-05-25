using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sandbox;

namespace Storm;

public static class Utils
{
	public static void RestartMap()
	{
		Game.AssertServer();
		Game.ChangeLevel(Game.Server.MapIdent);
	}

	public static async Task<JsonElement> ParseJson(string json)
	{
		MemoryStream stream = new(Encoding.UTF8.GetBytes(json));
		var document = await JsonDocument.ParseAsync(stream);
		return document.RootElement;
	}
}
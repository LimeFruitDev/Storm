using Sandbox;

namespace Storm;

public class Schema : GameManager
{
	public Schema()
	{
		Event.Run("Storm.Initialize");
		Event.Run(Game.IsServer ? "Storm.Initialize.Server" : "Storm.Initialize.Client");
	}

	public virtual string Author => "LimeFruit.Net";
	public virtual string Identifier => "base";
	public virtual string DisplayName => "Base Schema";
	public virtual string Description => "The base schema that all schemas derive from.";

	public static Schema Instance => Current as Schema;

	public override void Shutdown()
	{
		base.Shutdown();
		Event.Run("Storm.Shutdown");
		Event.Run(Game.IsServer ? "Storm.Shutdown.Server" : "Storm.Shutdown.Client");
	}

	public override void PostLevelLoaded()
	{
		base.PostLevelLoaded();
		Event.Run("Storm.PostLevelLoaded");
	}

	public override void RenderHud()
	{
		base.RenderHud();
		Event.Run("Storm.RenderHud");
	}

	public override void ClientJoined(IClient client)
	{
		base.ClientJoined(client);
		Event.Run("Storm.ClientJoined", client);
	}

	public override void ClientDisconnect(IClient client, NetworkDisconnectionReason reason)
	{
		base.ClientDisconnect(client, reason);
		Event.Run("Storm.ClientDisconnect", client, reason);
	}
}
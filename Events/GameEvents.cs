using Sandbox;

namespace Storm;

public static class GameEvent
{
	public class InitializeAttribute : EventAttribute
	{
		public InitializeAttribute() : base("Storm.Initialize")
		{
		}
	}

	public class ShutdownAttribute : EventAttribute
	{
		public ShutdownAttribute() : base("Storm.Shutdown")
		{
		}
	}

	public class PostLevelLoadedAttribute : EventAttribute
	{
		public PostLevelLoadedAttribute() : base("Storm.PostLevelLoaded")
		{
		}
	}

	public class RenderHudAttribute : EventAttribute
	{
		public RenderHudAttribute() : base("Storm.RenderHud")
		{
		}
	}

	public class ClientJoinedAttribute : EventAttribute
	{
		public ClientJoinedAttribute() : base("Storm.ClientJoined")
		{
		}
	}

	public class ClientDisconnectAttribute : EventAttribute
	{
		public ClientDisconnectAttribute() : base("Storm.ClientDisconnect")
		{
		}
	}

	public static class Initialize
	{
		public class ServerAttribute : EventAttribute
		{
			public ServerAttribute() : base("Storm.Initialize.Server")
			{
			}
		}

		public class ClientAttribute : EventAttribute
		{
			public ClientAttribute() : base("Storm.Initialize.Client")
			{
			}
		}
	}

	public static class Shutdown
	{
		public class ServerAttribute : EventAttribute
		{
			public ServerAttribute() : base("Storm.Shutdown.Server")
			{
			}
		}

		public class ClientAttribute : EventAttribute
		{
			public ClientAttribute() : base("Storm.Shutdown.Client")
			{
			}
		}
	}
}
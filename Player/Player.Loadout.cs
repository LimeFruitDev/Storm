using Sandbox;

namespace Storm;

public partial class Player
{
	[Net] [Predicted] public Entity ActiveChild { get; set; }
	[ClientInput] public Entity ActiveChildInput { get; set; }
	[Predicted] private Entity LastActiveChild { get; set; }

	public Loadout Loadout { get; protected set; }

	public virtual void SimulateActiveChild(IClient cl, Entity child)
	{
		if (LastActiveChild != child)
		{
			OnActiveChildChanged(LastActiveChild, child);
			LastActiveChild = child;
		}

		if (!LastActiveChild.IsValid())
			return;

		if (LastActiveChild.IsAuthority)
			LastActiveChild.Simulate(cl);
	}

	public virtual void OnActiveChildChanged(Entity previous, Entity next)
	{
		if (previous is Carriable previousBc)
			previousBc?.ActiveEnd(this, previousBc.Owner != this);

		if (next is Carriable nextBc)
			nextBc?.ActiveStart(this);
	}
}
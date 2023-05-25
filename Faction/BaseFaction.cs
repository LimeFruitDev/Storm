
using System.Collections.Generic;
using Sandbox;

namespace Storm;

public class BaseFaction : BaseNetworkable
{
	public virtual string UniqueId => "base";
	public virtual string Name => "Base Faction";
	public virtual string Description => "The base faction.";
	public virtual bool IsDefault => false;
	public virtual IList<string> Models => null;
}

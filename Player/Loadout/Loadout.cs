
using System;
using System.Collections.Generic;
using Sandbox;

namespace Storm;

// Made to replace the BaseInventory+IBaseInventory from thea base library.
public class Loadout
{
	public Entity Owner { get; set; }
	public List<Entity> List = new();

	public virtual Entity Active
	{
		get => (Owner as Player)?.ActiveChild;
		set
		{
			if (Owner is Player player)
				player.ActiveChild = value;
		}
	}

	public Loadout(Entity owner)
	{
		Owner = owner;
	}

	public virtual Entity GetSlot(int slot)
	{
		return (List.Count <= slot || slot < 0) ? null : List[slot];
	}

	public virtual int Count() => List.Count;

	public virtual bool CanAdd(Entity entity)
	{
		return (entity is Carriable carriable && carriable.CanCarry(Owner));
	}

	public virtual void OnChildAdded(Entity child)
	{
		if (!CanAdd(child)) return;

		if (List.Contains(child))
			throw new Exception("Trying to add to an inventory multiple times!");

		List.Add(child);
	}

	public virtual void OnChildRemoved(Entity child)
	{
		List.Remove(child);
	}

	public virtual bool Contains(Entity entity)
	{
		return List.Contains(entity);
	}

	public virtual bool Add(Entity entity, bool makeActive = false)
	{
		Game.AssertServer();

		if (entity.Owner != null || !CanAdd(entity))
			return false;

		entity.Parent = Owner;
		(entity as Carriable)?.OnCarryStart(Owner);

		if (makeActive)
			SetActive(entity);

		return true;
	}

	public virtual bool Drop(Entity entity)
	{
		if (!Game.IsServer || !Contains(entity))
			return false;

		entity.Parent = null;
		(entity as Carriable)?.OnCarryDrop(Owner);

		return true;
	}

	public virtual int GetActiveSlot()
	{
		for (var slot = 0; slot < List.Count; slot++)
		{
			if (List[slot] == Active)
				return slot;
		}

		return -1;
	}

	public virtual bool SetActiveSlot(int slot)
	{
		var ent = GetSlot(slot);
		if (ent == null || Active == ent) return false;

		Active = ent;
		return ent.IsValid();
	}

	public virtual bool SetActive(Entity entity)
	{
		if (Active == entity || !Contains(entity)) return false;
		Active = entity;
		return true;
	}

	public virtual Entity DropActive()
	{
		if (!Game.IsServer || Active == null) return null;

		var active = Active;
		if (!Drop(active)) return null;
		Active = null;
		return active;

	}
}

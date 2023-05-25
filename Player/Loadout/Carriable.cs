using Sandbox;

namespace Storm;

public class Carriable : AnimatedEntity
{
	public virtual string ViewModelPath => null;
	public BaseViewModel ViewModelEntity { get; protected set; }

	public virtual ModelEntity EffectEntity =>
		(ViewModelEntity.IsValid() && IsFirstPersonMode) ? ViewModelEntity : this;

	public override void Spawn()
	{
		base.Spawn();

		PhysicsEnabled = true;
		UsePhysicsCollision = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
	}

	public virtual bool CanCarry(Entity carrier)
	{
		return true;
	}

	public virtual void OnCarryStart(Entity carrier)
	{
		if (Game.IsClient) return;

		SetParent(carrier, true);
		Owner = carrier;
		EnableAllCollisions = false;
		EnableDrawing = false;
	}

	public virtual void OnCarryDrop(Entity carrier)
	{
		if (Game.IsClient) return;

		SetParent(null);
		Owner = null;
		EnableDrawing = true;
		EnableAllCollisions = true;
	}

	// TODO: Maybe don't necessarily use citizen animations in here.
	public virtual void SimulateAnimation(CitizenAnimationHelper animation)
	{
		animation.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
		animation.Handedness = CitizenAnimationHelper.Hand.Both;
		animation.AimBodyWeight = 1.0f;
	}

	public virtual void ActiveStart(Entity entity)
	{
		EnableDrawing = true;

		if (!IsLocalPawn) return;
		DestroyViewModel();
		DestroyHudElements();
		CreateViewModel();
		CreateHudElements();
	}

	public virtual void ActiveEnd(Entity entity, bool dropped)
	{
		if (!dropped)
			EnableDrawing = false;

		if (!Game.IsClient) return;
		DestroyViewModel();
		DestroyHudElements();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if (!Game.IsClient) return;
		DestroyViewModel();
		DestroyHudElements();
	}

	public virtual void CreateViewModel()
	{
		Game.AssertClient();

		if (string.IsNullOrEmpty(ViewModelPath))
			return;

		ViewModelEntity = new BaseViewModel
		{
			Position = Position,
			Owner = Owner,
			EnableViewmodelRendering = true
		};
		ViewModelEntity.SetModel(ViewModelPath);
	}

	public virtual void DestroyViewModel()
	{
		ViewModelEntity?.Delete();
		ViewModelEntity = null;
	}

	public virtual void CreateHudElements()
	{
	}

	public virtual void DestroyHudElements()
	{
	}
}

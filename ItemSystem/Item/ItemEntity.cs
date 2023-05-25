
using Sandbox;

namespace Storm;

public partial class ItemEntity : ModelEntity
{
	[Net] public ItemInstance Item { get; set; }

	public void LoadInstance()
	{
		SetModel(Item.WorldModel);
		Velocity = 0;

		EnableAllCollisions = true;
		EnableDrawing = true;

		SetupPhysicsFromModel(PhysicsMotionType.Keyframed);
		EnableHitboxes = true;
		PhysicsEnabled = true;
		Tags.Add("solid");

		ResetInterpolation();
	}

	public override void TakeDamage(DamageInfo damageInfo)
	{
		// TODO: Delete the entity and remove the instance if the health is too low?
	}
}

using System.ComponentModel;
using Sandbox;

namespace Storm;

// todo: this was literally ripped from base library, clean this up!!!
public partial class Player
{
	[ClientInput] public Vector3 InputDirection { get; protected set; }
	[ClientInput] public Angles ViewAngles { get; set; }
	public Angles OriginalViewAngles { get; private set; }

	[Net, Predicted, Browsable(false)]   public Vector3 EyeLocalPosition { get; set; }
	[Net, Predicted, Browsable(false)]   public Rotation EyeLocalRotation { get; set; }

	[Browsable(false)] public Vector3 EyePosition
	{
		get => Transform.PointToWorld(EyeLocalPosition);
		set => EyeLocalPosition = Transform.PointToLocal(value);
	}

	[Browsable(false)] public Rotation EyeRotation
	{
		get => Transform.RotationToWorld(EyeLocalRotation);
		set => EyeLocalRotation = Transform.RotationToLocal(value);
	}

	[Net, Predicted] public PawnController Controller { get; set; }
	[Net, Predicted] public PawnController DevController { get; set; }

	public virtual PawnController GetActiveController()
	{
		return DevController ?? Controller;
	}

	public override void Simulate(IClient cl)
	{
		if (ActiveChildInput.IsValid() && ActiveChildInput.Owner == this)
			ActiveChild = ActiveChildInput;

		if (!IsAlive)
			// todo
			//if (timeSinceDied > 3 && Game.IsServer)
			//{
			//	Respawn();
			//}
			return;

		var controller = GetActiveController();
		controller?.Simulate(cl, this);

		SimulateActiveChild(cl, ActiveChild);
	}

	public override void FrameSimulate(IClient cl)
	{
		Camera.Rotation = ViewAngles.ToRotation();
		Camera.Position = EyePosition;
		Camera.FieldOfView = Screen.CreateVerticalFieldOfView(Game.Preferences.FieldOfView);
		Camera.FirstPersonViewer = this;
	}

	public override void BuildInput()
	{
		OriginalViewAngles = ViewAngles;
		InputDirection = Input.AnalogMove;

		if (Input.StopProcessing)
			return;

		var look = Input.AnalogLook;
		if (ViewAngles.pitch is > 90f or < -90f)
			look = look.WithYaw(look.yaw * -1f);

		var viewAngles = ViewAngles;
		viewAngles += look;
		viewAngles.pitch = viewAngles.pitch.Clamp(-89f, 89f);
		viewAngles.roll = 0f;
		ViewAngles = viewAngles.Normal;

		ActiveChild?.BuildInput();
		GetActiveController()?.BuildInput();
	}

}
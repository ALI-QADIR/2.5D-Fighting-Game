namespace Smash.Player.States
{
	public class GroundEntry : PlayerBaseState
	{
		public GroundEntry(PlayerController controller) : base(controller)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_controller.SetOnGround();
		}
	}
	
	public class Idle : PlayerBaseState
	{
		public Idle(PlayerController controller) : base(controller)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_controller.CurrentState = this;
		}
	}
	
	public class Moving : PlayerBaseState
	{
		public Moving(PlayerController controller) : base(controller)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_controller.CurrentState = this;
		}
	}
    
	public class GroundExit : PlayerBaseState
	{
		public GroundExit(PlayerController controller) : base(controller)
		{
		}
	}
}
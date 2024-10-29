namespace Smash.Player.States
{
	public class AirEntry : PlayerBaseState
	{
		public AirEntry(PlayerController controller) : base(controller)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_controller.SetInAir();
		}
	}
	
	public class Rising : PlayerBaseState
	{
		public Rising(PlayerController controller) : base(controller)
		{
		}
	}
	
	public class Falling : PlayerBaseState
	{
		public Falling(PlayerController controller) : base(controller)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_controller.CheckForCoyote();
		}
	}
    
	public class AirExit : PlayerBaseState
	{
		public AirExit(PlayerController controller) : base(controller)
		{
		}
	}
}
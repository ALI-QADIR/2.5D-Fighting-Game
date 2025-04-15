namespace Smash.Player.States
{
	public class GroundEntry : PlayerBaseState
	{
		public GroundEntry(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.SetOnGround();
		}
	}
	
	public class Idle : PlayerBaseState
	{
		public Idle(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetIdle();
		}
	}
	
	public class Moving : PlayerBaseState
	{
		public Moving(PlayerPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetRunning();
		}
	}

	public class GroundExit : PlayerBaseState
	{
		public GroundExit(PlayerPawn pawn) : base(pawn)
		{
		}
	}
}
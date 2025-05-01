namespace Smash.Player.States
{
	public class GroundEntry : PlayerBaseState
	{
		public GroundEntry(CharacterPawn pawn) : base(pawn)
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
		public Idle(CharacterPawn pawn) : base(pawn)
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
		public Moving(CharacterPawn pawn) : base(pawn)
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
		public GroundExit(CharacterPawn pawn) : base(pawn)
		{
		}
	}
}
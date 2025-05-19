using Smash.Player.Components;

namespace Smash.Player.States
{
	public class GroundEntry : PlayerBaseState
	{
		public GroundEntry(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.SetOnGround();
			_graphicsController.SetOnGround();
		}
	}
	
	public class Idle : PlayerBaseState
	{
		public Idle(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_graphicsController.SetIdle();
		}
	}
	
	public class Moving : PlayerBaseState
	{
		public Moving(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_graphicsController.SetRunning();
		}
	}

	public class GroundExit : PlayerBaseState
	{
		public GroundExit(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
	}
}
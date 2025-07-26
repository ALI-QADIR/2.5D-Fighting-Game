using Smash.Player.Components;

namespace Smash.Player.States
{
	public class HurtState : PlayerBaseState
	{
		public HurtState(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn,
			graphicsController)
		{
		}
	}
	public class KnockBack : HurtState
	{
		public KnockBack(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_graphicsController.SetKnockBack();
		}
	}
	
	public class TossUpStart : HurtState
	{
		public TossUpStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_graphicsController.SetTossUp();
		}
	}
	
	public class TossUpEnd : HurtState
	{
		public TossUpEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
		}
	}
	
	public class Stun : HurtState
	{
		public Stun(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
		}
	}
}

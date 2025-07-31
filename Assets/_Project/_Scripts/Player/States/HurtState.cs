using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.States
{
	public class HurtState : PlayerBaseState
	{
		protected HurtState(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn,
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
		public float ElapseTime { get; private set; }
		public TossUpStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_graphicsController.SetTossUpStart();
			ElapseTime = 0f;
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapseTime += Time.deltaTime;
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
			_graphicsController.SetTossUpFinish();
		}
	}
	
	public class Stun : HurtState
	{
		public float ElapsedTime { get; private set; }
		public Stun(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_graphicsController.SetStun(true);
			ElapsedTime = 0f;
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}

		public override void OnExit()
		{
			base.OnExit();
			_graphicsController.SetStun(false);
			_pawn.IsStunned = false;
		}
	}
}

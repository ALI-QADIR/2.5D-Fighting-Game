using UnityEngine;

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

	public class Dash : PlayerBaseState
	{
		private readonly float m_duration;
		private float m_elapsedTime;
		public bool IsFinished => m_elapsedTime >= m_duration;
		
		public Dash(PlayerPawn pawn, float duration) : base(pawn)
		{
			m_duration = duration;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			m_elapsedTime = 0f;
			_pawn.CurrentState = this;
			_pawn.SetDashStart();
		}
		
		public override void OnUpdate()
		{
			base.OnUpdate();
			m_elapsedTime += Time.deltaTime;
		}

		public override void OnExit()
		{
			base.OnExit();
			m_elapsedTime = 0f;
			_pawn.SetDashEnd();
		}
	}
    
	public class GroundExit : PlayerBaseState
	{
		public GroundExit(PlayerPawn pawn) : base(pawn)
		{
		}
	}
}
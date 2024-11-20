using UnityEngine;

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
			_controller.SetIdle();
		}
	}

	public class Rotating : PlayerBaseState
	{
		private readonly float m_duration;
		private float m_elapsedTime;
		public bool IsFinished => m_elapsedTime >= m_duration;
		public Rotating(PlayerController controller, float duration) : base(controller)
		{
			m_duration = duration;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			m_elapsedTime = 0f;
			_controller.CurrentState = this;
		}

		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			m_elapsedTime += Time.deltaTime;
			_controller.HandleRotation(m_elapsedTime / m_duration);
		}

		public override void OnExit()
		{
			base.OnExit();
			m_elapsedTime = 0f;
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
			_controller.SetRunning();
		}
	}

	public class Dash : PlayerBaseState
	{
		private readonly float m_duration;
		private float m_elapsedTime;
		public bool IsFinished => m_elapsedTime >= m_duration;
		
		public Dash(PlayerController controller, float duration) : base(controller)
		{
			m_duration = duration;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			m_elapsedTime = 0f;
			_controller.CurrentState = this;
			_controller.SetDashStart();
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
			_controller.SetDashEnd();
		}
	}
    
	public class GroundExit : PlayerBaseState
	{
		public GroundExit(PlayerController controller) : base(controller)
		{
		}
	}
}
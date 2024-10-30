using System;
using UnityEngine;

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

		public override void OnEnter()
		{
			base.OnEnter();
			_controller.CurrentState = this;
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
			_controller.CurrentState = this;
		}
	}
	
	public class Coyote : PlayerBaseState
	{
		public float ElapsedTime { get; private set; }

		public Coyote(PlayerController controller) : base(controller)
		{
		}


		public override void OnEnter()
		{
			base.OnEnter();
			ElapsedTime = 0f;
			_controller.CurrentState = this;
		}
		
		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
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
    
	public class AirExit : PlayerBaseState
	{
		public AirExit(PlayerController controller) : base(controller)
		{
		}
	}
}
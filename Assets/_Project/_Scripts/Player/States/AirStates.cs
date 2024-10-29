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
	}
	
	public class Falling : PlayerBaseState
	{
		public Falling(PlayerController controller) : base(controller)
		{
		}
	}
	
	public class Coyote : PlayerBaseState
	{
		private float m_elapsedTime;
		public float ElapsedTime => m_elapsedTime;
		
		public Coyote(PlayerController controller) : base(controller)
		{
		}


		public override void OnEnter()
		{
			base.OnEnter();
			m_elapsedTime = 0f;
			_controller.SetCoyote(true);
		}
		
		public override void OnUpdate()
		{
			base.OnUpdate();
			m_elapsedTime += Time.deltaTime;
		}

		public override void OnExit()
		{
			base.OnExit();
			_controller.SetCoyote(false);
		}
	}
    
	public class AirExit : PlayerBaseState
	{
		public AirExit(PlayerController controller) : base(controller)
		{
		}
	}
}
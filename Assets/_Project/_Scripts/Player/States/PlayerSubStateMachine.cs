using TripleA.FSM;

namespace Smash.Player.States
{
	public class PlayerSubStateMachine : PlayerBaseState
	{
		protected StateMachine _stateMachine;
		protected PlayerSubStateMachine(PlayerController controller) : base(controller)
		{
		}
	}
	
	public class Grounded : PlayerSubStateMachine
	{
		protected Grounded(PlayerController controller) : base(controller)
		{
			SetUpStateMachine();
		}

		private void SetUpStateMachine()
		{
			_stateMachine = new StateMachine();
		}
	}
	
	public class Airborne : PlayerSubStateMachine
	{
		protected Airborne(PlayerController controller) : base(controller)
		{
		}
		
		private void SetUpStateMachine()
		{
			_stateMachine = new StateMachine();
		}
	}
}
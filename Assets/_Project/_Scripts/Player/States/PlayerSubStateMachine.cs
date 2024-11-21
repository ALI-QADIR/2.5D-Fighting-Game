using TripleA.FSM;

namespace Smash.Player.States
{
	public class PlayerSubStateMachine : PlayerBaseState
	{
		protected StateMachine _stateMachine;
		protected bool _dashPressed;
		
		protected PlayerSubStateMachine(PlayerController controller) : base(controller)
		{
		}
		
		protected virtual void CreateStates() {}
		protected virtual void CreateTransitions() {}
		protected virtual void AddTransitions() {}
		

		protected bool DashPredicate<T>()
		{
			bool flag = _stateMachine.CurrentState is T && _dashPressed;
			_dashPressed = false;
			return flag;
		}

		protected void ControllerOnOnDash(bool value)
		{
			_dashPressed = value;
		}
		
		protected void AddTransition(IState from, IState to, IPredicate condition) =>
			_stateMachine.AddTransition(from, to, condition);
		
		protected void AddAnyTransition(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
	}
}
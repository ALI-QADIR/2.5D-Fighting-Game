using TripleA.FSM;

namespace Smash.Player.States
{
	public class PlayerSubStateMachine : PlayerBaseState
	{
		protected StateMachine _stateMachine;
		protected PlayerSubStateMachine(PlayerController controller, PlayerAnimator animator) : base(controller, animator)
		{
		}
		
		protected virtual void CreateStates() {}
		protected virtual void CreateTransitions() {}
		protected virtual void AddTransitions() {}
		
		
		protected void AddTransition(IState from, IState to, IPredicate condition) =>
			_stateMachine.AddTransition(from, to, condition);
		
		protected void AddAnyTransition(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
	}
}
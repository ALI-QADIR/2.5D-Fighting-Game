using TripleA.FSM;

namespace Smash.Player.States
{
	public abstract class PlayerSubStateMachine : PlayerBaseState
	{
		protected StateMachine _stateMachine;
		protected bool _dashPressed;

		public bool MainAttackTap { get; set; }
		public bool MainAttackHold { get; set; }

		protected PlayerSubStateMachine(PlayerPawn pawn) : base(pawn)
		{
		}
		
		protected virtual void CreateStates() {}
		protected virtual void CreateTransitions() {}
		protected virtual void AddTransitions() {}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentStateMachine = this;
		}


		protected bool DashPredicate<T>()
		{
			bool flag = _stateMachine.CurrentState is T && _dashPressed;
			_dashPressed = false;
			return flag;
		}

		protected void PawnOnOnDash(bool value)
		{
			_dashPressed = value;
		}
		
		protected void AddTransition(IState from, IState to, IPredicate condition) =>
			_stateMachine.AddTransition(from, to, condition);
		
		protected void AddAnyTransition(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
	}
}
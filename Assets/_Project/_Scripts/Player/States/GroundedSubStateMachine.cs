using TripleA.FSM;

namespace Smash.Player.States
{
	public sealed class GroundedSubStateMachine : PlayerSubStateMachine
	{
		private GroundEntry m_groundEntry;
		private GroundExit m_groundExit;
		private Idle m_idle;
		private Moving m_moving;
		private Dash m_dash;

		private bool m_dashPressed;
		
		private readonly FuncPredicate m_entryToIdleCondition;
		private readonly FuncPredicate m_entryToMovingCondition;
		private readonly FuncPredicate m_idleToMovingCondition;
		private readonly FuncPredicate m_movingToIdleCondition;
		private readonly FuncPredicate m_dashToIdleCondition;
		private readonly FuncPredicate m_dashToMovingCondition;
		private readonly FuncPredicate m_idleToDashCondition;
		private readonly FuncPredicate m_movingToDashCondition;
		
		public GroundedSubStateMachine(PlayerController controller) : base(controller)
		{
			_stateMachine = new StateMachine();

			CreateStates();
			
			m_entryToIdleCondition = new FuncPredicate(() => _stateMachine.CurrentState is GroundEntry && !_controller.IsMoving());
			m_entryToMovingCondition = new FuncPredicate(() => _stateMachine.CurrentState is GroundEntry && _controller.IsMoving());
			
			m_idleToMovingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle && _controller.IsMoving());
			m_movingToIdleCondition = new FuncPredicate(() => _stateMachine.CurrentState is Moving && !_controller.IsMoving());
			
			m_idleToDashCondition = new FuncPredicate(Predicate<Idle>);
			m_movingToDashCondition = new FuncPredicate(Predicate<Moving>);
			
			m_dashToIdleCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Dash && !_controller.IsMoving() && m_dash.IsFinished);
			m_dashToMovingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Dash && _controller.IsMoving() && m_dash.IsFinished);
			
			CreateAndAddTransitions();
			return;

			bool Predicate<T>()
			{
				bool flag = _stateMachine.CurrentState is T && m_dashPressed;
				m_dashPressed = false;
				return flag;
			}
		}

		#region State Machine Methods

		public override void OnEnter()
		{
			base.OnEnter();
			_controller.OnDash += ControllerOnOnDash;
			_controller.CurrentState = this;
			_stateMachine.SetState(m_groundEntry);
			
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			_stateMachine.OnUpdate();
		}

		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			_stateMachine.OnFixedUpdate();
		}
		
		public override void OnLateUpdate()
		{
			base.OnLateUpdate();
			_stateMachine.OnLateUpdate();
		}

		public override void OnExit()
		{
			base.OnExit();
			_controller.OnDash -= ControllerOnOnDash;
			_stateMachine.SetState(m_groundExit);
		}

		#endregion State Machine Methods

		private void ControllerOnOnDash(bool value)
		{
			m_dashPressed = value;
		}

		protected override void CreateStates()
		{
			m_groundEntry = new GroundEntry(_controller);
			m_groundExit = new GroundExit(_controller);
			m_idle = new Idle(_controller);
			m_moving = new Moving(_controller);
			m_dash = new Dash(_controller, _controller.DashDuration);
		}

		protected override void CreateAndAddTransitions()
		{
			AddTransition(m_groundEntry, m_idle, m_entryToIdleCondition);
			AddTransition(m_groundEntry, m_moving, m_entryToMovingCondition);
			AddTransition(m_idle, m_moving, m_idleToMovingCondition);
			AddTransition(m_moving, m_idle, m_movingToIdleCondition);
			AddTransition(m_idle, m_dash, m_idleToDashCondition);
			AddTransition(m_moving, m_dash, m_movingToDashCondition);
			AddTransition(m_dash, m_idle, m_dashToIdleCondition);
			AddTransition(m_dash, m_moving, m_dashToMovingCondition);
			
			AddTransition(m_groundExit, m_groundEntry, new FuncPredicate(() => false));
		}
	}
}
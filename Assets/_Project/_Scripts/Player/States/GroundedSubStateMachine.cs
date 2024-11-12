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
		private Rotating m_rotating;

		private bool m_dashPressed;
		private bool m_rotatePressed;
		
		private FuncPredicate m_entryToIdleCondition;
		private FuncPredicate m_entryToMovingCondition;
		private FuncPredicate m_idleToMovingCondition;
		private FuncPredicate m_movingToIdleCondition;
		private FuncPredicate m_dashToIdleCondition;
		private FuncPredicate m_dashToMovingCondition;
		private FuncPredicate m_idleToDashCondition;
		private FuncPredicate m_movingToDashCondition;
		
		public GroundedSubStateMachine(PlayerController controller, PlayerAnimator animator) : base(controller, animator)
		{
			_stateMachine = new StateMachine();

			CreateStates();
			
			CreateTransitions();
			
			AddTransitions();
		}

		#region State Machine Methods

		public override void OnEnter()
		{
			base.OnEnter();
			_controller.OnDash += ControllerOnOnDash;
			_controller.OnRotate += ControllerOnRotate;
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
			_controller.OnRotate -= ControllerOnRotate;
			_stateMachine.SetState(m_groundExit);
		}

		#endregion State Machine Methods

		private void ControllerOnOnDash(bool value)
		{
			m_dashPressed = value;
		}

		private void ControllerOnRotate()
		{
			m_rotatePressed = true;
		}

		private bool DashPredicate<T>()
		{
			bool flag = _stateMachine.CurrentState is T && m_dashPressed;
			m_dashPressed = false;
			return flag;
		}
		
		private bool RotatingPredicate<T>()
		{
			bool flag = _stateMachine.CurrentState is T && m_rotatePressed;
			m_rotatePressed = false;
			return flag;
		}

		protected override void CreateStates()
		{
			m_groundEntry = new GroundEntry(_controller, _animator);
			m_groundExit = new GroundExit(_controller, _animator);
			m_idle = new Idle(_controller, _animator);
			m_moving = new Moving(_controller, _animator);
			m_dash = new Dash(_controller, _controller.DashDuration, _animator);
			m_rotating = new Rotating(_controller, _controller.TimeToRotate, _animator);
		}

		private FuncPredicate m_idleToRotatingCondition;
		private FuncPredicate m_movingToRotatingCondition;
		private FuncPredicate m_entryToRotatingCondition;
		private FuncPredicate m_rotatingToIdleCondition;
		private FuncPredicate m_rotatingToMovingCondition;

		protected override void CreateTransitions()
		{
			m_entryToIdleCondition = new FuncPredicate(() => _stateMachine.CurrentState is GroundEntry && !_controller.IsMoving());
			m_entryToMovingCondition = new FuncPredicate(() => _stateMachine.CurrentState is GroundEntry && _controller.IsMoving());
			m_movingToRotatingCondition = new FuncPredicate(RotatingPredicate<GroundEntry>);
			
			m_idleToMovingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle && _controller.IsMoving());
			m_idleToDashCondition = new FuncPredicate(DashPredicate<Idle>);
			m_idleToRotatingCondition = new FuncPredicate(RotatingPredicate<Idle>);
			
			m_movingToIdleCondition = new FuncPredicate(() => _stateMachine.CurrentState is Moving && !_controller.IsMoving());
			m_movingToDashCondition = new FuncPredicate(DashPredicate<Moving>);
			m_movingToRotatingCondition = new FuncPredicate(RotatingPredicate<Moving>);
			
			m_rotatingToIdleCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Rotating && !_controller.IsMoving() && m_rotating.IsFinished);
			m_rotatingToMovingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Rotating && _controller.IsMoving() && m_rotating.IsFinished);
			
			m_dashToIdleCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Dash && !_controller.IsMoving() && m_dash.IsFinished);
			m_dashToMovingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Dash && _controller.IsMoving() && m_dash.IsFinished);
		}

		protected override void AddTransitions()
		{
			AddTransition(m_groundEntry, m_idle, m_entryToIdleCondition);
			AddTransition(m_groundEntry, m_moving, m_entryToMovingCondition);
			AddTransition(m_groundEntry, m_rotating, m_entryToMovingCondition);
			
			AddTransition(m_idle, m_moving, m_idleToMovingCondition);
			AddTransition(m_idle, m_dash, m_idleToDashCondition);
			AddTransition(m_idle, m_rotating, m_idleToRotatingCondition);
			
			AddTransition(m_moving, m_idle, m_movingToIdleCondition);
			AddTransition(m_moving, m_dash, m_movingToDashCondition);
			AddTransition(m_moving, m_rotating, m_movingToRotatingCondition);
			
			AddTransition(m_rotating, m_idle, m_rotatingToIdleCondition);
			AddTransition(m_rotating, m_moving, m_rotatingToMovingCondition);
			
			AddTransition(m_dash, m_idle, m_dashToIdleCondition);
			AddTransition(m_dash, m_moving, m_dashToMovingCondition);
			
			AddTransition(m_groundExit, m_groundEntry, new FuncPredicate(() => false));
		}
	}
}
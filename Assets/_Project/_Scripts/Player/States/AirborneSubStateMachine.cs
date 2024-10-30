using TripleA.FSM;

namespace Smash.Player.States
{
	public sealed class AirborneSubStateMachine : PlayerSubStateMachine
	{
		private AirEntry m_airEntry;
		private AirExit m_airExit;
		private Rising m_rising;
		private Falling m_falling;
		private Coyote m_coyote;
		private Dash m_dash;
		
		private bool m_dashPressed;

		private readonly FuncPredicate m_entryToRisingCondition;
		private readonly FuncPredicate m_entryToCoyoteCondition;
		
		private readonly FuncPredicate m_risingToFallingCondition;
		private readonly FuncPredicate m_risingToDashCondition;
		private readonly FuncPredicate m_fallingToRisingCondition;
		private readonly FuncPredicate m_fallingToDashCondition;
		
		private readonly FuncPredicate m_coyoteToRisingCondition;
		private readonly FuncPredicate m_coyoteToFallingCondition;
		private readonly FuncPredicate m_coyoteToDashCondition;
		
		private readonly FuncPredicate m_dashToCoyoteCondition;

		public AirborneSubStateMachine(PlayerController controller) : base(controller)
		{
			_stateMachine = new StateMachine();
			
			CreateStates();
			
			m_entryToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _controller.IsRising());
			m_entryToCoyoteCondition = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _controller.IsFalling());
			
			m_risingToFallingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Rising && _controller.IsFalling());
			m_fallingToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Falling && _controller.IsRising());
			
			m_risingToDashCondition = new FuncPredicate(Predicate<Rising>);
			m_fallingToDashCondition = new FuncPredicate(Predicate<Falling>);
			
			m_dashToCoyoteCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Dash && m_dash.IsFinished);
			
			m_coyoteToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Coyote && _controller.IsRising());
			m_coyoteToDashCondition = new FuncPredicate(Predicate<Coyote>);
			m_coyoteToFallingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote && _controller.IsFalling() &&
				m_coyote.ElapsedTime >= _controller.CoyoteTime);
			
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
			_controller.CurrentState = this;
			_controller.OnDash += ControllerOnOnDash;
			_stateMachine.SetState(m_airEntry);
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
			_stateMachine.SetState(m_airExit);
		}

		#endregion State Machine Methods
		
		private void ControllerOnOnDash(bool value)
		{
			m_dashPressed = value;
		}
		
		protected override void CreateStates()
		{
			m_airEntry = new AirEntry(_controller);
			m_airExit = new AirExit(_controller);
			m_rising = new Rising(_controller);
			m_falling = new Falling(_controller);
			m_coyote = new Coyote(_controller);
			m_dash = new Dash(_controller, _controller.DashDuration);
		}

		protected override void CreateAndAddTransitions()
		{
			AddTransition(m_airEntry, m_rising, m_entryToRisingCondition);
			AddTransition(m_airEntry, m_coyote, m_entryToCoyoteCondition);
			
			AddTransition(m_rising, m_falling, m_risingToFallingCondition);
			AddTransition(m_falling, m_rising, m_fallingToRisingCondition);
			
			AddTransition(m_rising, m_dash, m_risingToDashCondition);
			AddTransition(m_falling, m_dash, m_fallingToDashCondition);
			
			AddTransition(m_dash, m_coyote, m_dashToCoyoteCondition);
			
			AddTransition(m_coyote, m_falling, m_coyoteToFallingCondition);
			AddTransition(m_coyote, m_rising, m_coyoteToRisingCondition);
			AddTransition(m_coyote, m_dash, m_coyoteToDashCondition);
			
			AddTransition(m_airExit, m_airEntry, new FuncPredicate(() => false));
		}
	}
}
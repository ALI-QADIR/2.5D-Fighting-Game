using TripleA.FSM;

namespace Smash.Player.States
{
	public sealed class AirborneSubStateMachine : PlayerSubStateMachine
	{
		private AirEntry m_airEntry;
		private AirExit m_airExit;
		private Rising m_rising;
		private Falling m_falling;
		private FloatingFall m_floatingFall;
		private CrashingFall m_crashingFall;
		private Coyote m_coyote;
		private Dash m_dash;
		private Apex m_apex;
		private Ledge m_ledge;

		private FuncPredicate m_entryToRisingCondition;
		private FuncPredicate m_entryToCoyoteCondition;
		
		private FuncPredicate m_risingToApexCondition;
		private FuncPredicate m_risingToDashCondition;
		
		private FuncPredicate m_apexToFallingCondition;
		private FuncPredicate m_apexToFloatingCondition;
		private FuncPredicate m_apexToCrashingCondition;
		private FuncPredicate m_apexToRisingCondition;
		private FuncPredicate m_apexToDashCondition;
		
		private FuncPredicate m_ledgeCondition;
		private FuncPredicate m_crashingCondition;
		private FuncPredicate m_floatingToDashCondition;
		
		private FuncPredicate m_fallingToRisingCondition;
		private FuncPredicate m_fallingToDashCondition;
		
		private FuncPredicate m_ledgeToRisingCondition;
		
		private FuncPredicate m_coyoteToRisingCondition;
		private FuncPredicate m_coyoteToFallingCondition;
		private FuncPredicate m_coyoteToDashCondition;
		
		private FuncPredicate m_dashToCoyoteCondition;

		public AirborneSubStateMachine(PlayerController controller) : base(controller)
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
		
		protected override void CreateStates()
		{
			m_airEntry = new AirEntry(_controller);
			m_airExit = new AirExit(_controller);
			m_rising = new Rising(_controller);
			m_falling = new Falling(_controller);
			m_floatingFall = new FloatingFall(_controller);
			m_crashingFall = new CrashingFall(_controller);
			m_coyote = new Coyote(_controller);
			m_dash = new Dash(_controller, _controller.DashDuration);
			m_apex = new Apex(_controller);
			m_ledge = new Ledge(_controller);
		}
		
		protected override void CreateTransitions()
		{
			m_entryToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _controller.IsRising());
			m_entryToCoyoteCondition = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _controller.IsFalling());
			
			m_risingToApexCondition = new FuncPredicate(() => _stateMachine.CurrentState is Rising && _controller.IsFalling());
			m_risingToDashCondition = new FuncPredicate(DashPredicate<Rising>);
			
			m_fallingToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Falling && _controller.IsRising());
			m_fallingToDashCondition = new FuncPredicate(DashPredicate<Falling>);

			m_ledgeCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote or FloatingFall or Falling or Apex && _controller.IsLedgeGrab());
			m_crashingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Ledge or FloatingFall or Falling or Dash or Rising && _controller.IsCrashingFall());

			m_apexToDashCondition = new FuncPredicate(DashPredicate<Apex>);
			m_apexToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Apex && _controller.IsRising());
			m_apexToFallingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Apex && m_apex.ElapsedTime >= _controller.ApexTime && _controller.IsNormalFall());
			m_apexToFloatingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Apex && m_apex.ElapsedTime >= _controller.ApexTime && _controller.IsFloatingFall());
			m_apexToCrashingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Apex && m_apex.ElapsedTime >= _controller.ApexTime && _controller.IsCrashingFall());
			
			m_dashToCoyoteCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Dash && m_dash.IsFinished);
			m_floatingToDashCondition = new FuncPredicate(DashPredicate<FloatingFall>);
			
			m_ledgeToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Ledge && _controller.IsRising());
			
			m_coyoteToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Coyote && _controller.IsRising());
			m_coyoteToDashCondition = new FuncPredicate(DashPredicate<Coyote>);
			m_coyoteToFallingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote && _controller.IsFalling() && m_coyote.ElapsedTime >= _controller.CoyoteTime);
		}

		protected override void AddTransitions()
		{
			AddTransition(m_airEntry, m_rising, m_entryToRisingCondition);
			AddTransition(m_airEntry, m_coyote, m_entryToCoyoteCondition);
			
			AddTransition(m_rising, m_apex, m_risingToApexCondition);
			AddTransition(m_rising, m_dash, m_risingToDashCondition);
			AddTransition(m_rising, m_crashingFall, m_crashingCondition);
			
			AddTransition(m_falling, m_rising, m_fallingToRisingCondition);
			AddTransition(m_falling, m_dash, m_fallingToDashCondition);
			AddTransition(m_falling, m_crashingFall, m_crashingCondition);
			AddTransition(m_falling, m_ledge, m_ledgeCondition);
			
			AddTransition(m_apex, m_dash, m_apexToDashCondition);
			AddTransition(m_apex, m_rising, m_apexToRisingCondition);
			AddTransition(m_apex, m_falling, m_apexToFallingCondition);
			AddTransition(m_apex, m_floatingFall, m_apexToFloatingCondition);
			AddTransition(m_apex, m_crashingFall, m_apexToCrashingCondition);
			AddTransition(m_apex, m_ledge, m_ledgeCondition);
			
			AddTransition(m_dash, m_coyote, m_dashToCoyoteCondition);
			AddTransition(m_dash, m_crashingFall, m_crashingCondition);
			
			AddTransition(m_floatingFall, m_crashingFall, m_crashingCondition);
			AddTransition(m_floatingFall, m_dash, m_floatingToDashCondition);
			AddTransition(m_floatingFall, m_ledge, m_ledgeCondition);
			
			AddTransition(m_ledge, m_crashingFall, m_crashingCondition);
			AddTransition(m_ledge, m_rising, m_ledgeToRisingCondition);
			
			AddTransition(m_coyote, m_falling, m_coyoteToFallingCondition);
			AddTransition(m_coyote, m_rising, m_coyoteToRisingCondition);
			AddTransition(m_coyote, m_dash, m_coyoteToDashCondition);
			AddTransition(m_coyote, m_ledge, m_ledgeCondition);
			
			AddTransition(m_airExit, m_airEntry, new FuncPredicate(() => false));
		}
	}
}
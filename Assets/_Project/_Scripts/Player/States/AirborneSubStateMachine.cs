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
		private WallSlide m_wallSlide;

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
		private FuncPredicate m_wallCondition;
		private FuncPredicate m_crashingCondition;
		private FuncPredicate m_floatingToDashCondition;
		
		private FuncPredicate m_fallingToRisingCondition;
		private FuncPredicate m_fallingToDashCondition;
		
		private FuncPredicate m_ledgeToRisingCondition;
		
		private FuncPredicate m_wallSlideToRisingCondition;
		private FuncPredicate m_wallSlideToFallingCondition;
		
		private FuncPredicate m_coyoteToRisingCondition;
		private FuncPredicate m_coyoteToFallingCondition;
		private FuncPredicate m_coyoteToDashCondition;
		
		private FuncPredicate m_dashToCoyoteCondition;

		public AirborneSubStateMachine(PlayerPawn pawn) : base(pawn)
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
			_pawn.CurrentState = this;
			_pawn.OnDash += PawnOnOnDash;
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
			_pawn.OnDash -= PawnOnOnDash;
			_stateMachine.SetState(m_airExit);
		}

		#endregion State Machine Methods
		
		protected override void CreateStates()
		{
			m_airEntry = new AirEntry(_pawn);
			m_airExit = new AirExit(_pawn);
			m_rising = new Rising(_pawn);
			m_falling = new Falling(_pawn);
			m_floatingFall = new FloatingFall(_pawn);
			m_crashingFall = new CrashingFall(_pawn);
			m_coyote = new Coyote(_pawn);
			m_dash = new Dash(_pawn, _pawn.DashDuration);
			m_apex = new Apex(_pawn);
			m_ledge = new Ledge(_pawn);
			m_wallSlide = new WallSlide(_pawn);
		}
		
		protected override void CreateTransitions()
		{
			// Entry
			m_entryToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _pawn.IsRising());
			m_entryToCoyoteCondition = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _pawn.IsFalling());
			
			// Rising
			m_risingToApexCondition = new FuncPredicate(() => _stateMachine.CurrentState is Rising && _pawn.IsFalling());
			m_risingToDashCondition = new FuncPredicate(DashPredicate<Rising>);
			
			// Falling
			m_fallingToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Falling && _pawn.IsRising());
			m_fallingToDashCondition = new FuncPredicate(DashPredicate<Falling>);

			// Independent
			m_ledgeCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote or FloatingFall or Falling or Apex or WallSlide && _pawn.IsLedgeGrab());
			m_wallCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is FloatingFall or Falling && _pawn.IsWallDetected());
			m_crashingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Ledge or FloatingFall or Falling or Dash or Rising or WallSlide && _pawn.IsCrashingFall());

			// Apex
			m_apexToDashCondition = new FuncPredicate(DashPredicate<Apex>);
			m_apexToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Apex && _pawn.IsRising());
			m_apexToFallingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Apex && m_apex.ElapsedTime >= _pawn.ApexTime && _pawn.IsNormalFall());
			m_apexToFloatingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Apex && m_apex.ElapsedTime >= _pawn.ApexTime && _pawn.IsFloatingFall());
			m_apexToCrashingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Apex && m_apex.ElapsedTime >= _pawn.ApexTime && _pawn.IsCrashingFall());
			
			// Dash
			m_dashToCoyoteCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Dash && m_dash.IsFinished);
			m_floatingToDashCondition = new FuncPredicate(DashPredicate<FloatingFall>);
			
			// Ledge
			m_ledgeToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Ledge && _pawn.IsRising());
			
			// WallSlide
			m_wallSlideToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is WallSlide && _pawn.IsRising());
			m_wallSlideToFallingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is WallSlide && _pawn.IsFalling() && !_pawn.IsWallDetected());
			
			// Coyote
			m_coyoteToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Coyote && _pawn.IsRising());
			m_coyoteToDashCondition = new FuncPredicate(DashPredicate<Coyote>);
			m_coyoteToFallingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote && _pawn.IsFalling() && m_coyote.ElapsedTime >= _pawn.CoyoteTime);
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
			AddTransition(m_falling, m_wallSlide, m_wallCondition);
			
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
			AddTransition(m_floatingFall, m_wallSlide, m_wallCondition);
			
			AddTransition(m_ledge, m_crashingFall, m_crashingCondition);
			AddTransition(m_ledge, m_rising, m_ledgeToRisingCondition);
			
			AddTransition(m_wallSlide, m_crashingFall, m_crashingCondition);
			AddTransition(m_wallSlide, m_falling, m_wallSlideToFallingCondition);
			AddTransition(m_wallSlide, m_rising, m_wallSlideToRisingCondition);
			AddTransition(m_wallSlide, m_ledge, m_ledgeCondition);
			
			
			AddTransition(m_coyote, m_falling, m_coyoteToFallingCondition);
			AddTransition(m_coyote, m_rising, m_coyoteToRisingCondition);
			AddTransition(m_coyote, m_dash, m_coyoteToDashCondition);
			AddTransition(m_coyote, m_ledge, m_ledgeCondition);
			
			AddTransition(m_airExit, m_airEntry, new FuncPredicate(() => false));
		}
	}
}

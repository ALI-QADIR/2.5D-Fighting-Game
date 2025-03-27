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
		private Apex m_apex;
		private Ledge m_ledge;
		private WallSlide m_wallSlide;

		private FuncPredicate m_entryToRisingCondition;
		private FuncPredicate m_entryToCoyoteCondition;
		
		private FuncPredicate m_risingToApexCondition;
		
		private FuncPredicate m_apexToFallingCondition;
		private FuncPredicate m_apexToRisingCondition;
		
		private FuncPredicate m_ledgeCondition;
		private FuncPredicate m_wallCondition;
		
		private FuncPredicate m_fallingToRisingCondition;
		
		private FuncPredicate m_ledgeToRisingCondition;
		
		private FuncPredicate m_wallSlideToRisingCondition;
		private FuncPredicate m_wallSlideToFallingCondition;
		
		private FuncPredicate m_coyoteToRisingCondition;
		private FuncPredicate m_coyoteToFallingCondition;

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
			m_coyote = new Coyote(_pawn);
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
			
			// Falling
			m_fallingToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Falling && _pawn.IsRising());

			// Independent
			m_ledgeCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote or Falling or Apex or WallSlide && _pawn.IsLedgeGrab());
			m_wallCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Falling && _pawn.IsWallDetected());

			// Apex
			m_apexToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Apex && _pawn.IsRising());
			m_apexToFallingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Apex && m_apex.ElapsedTime >= _pawn.ApexTime && _pawn.IsFalling());
			
			// Ledge
			m_ledgeToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Ledge && _pawn.IsRising());
			
			// WallSlide
			m_wallSlideToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is WallSlide && _pawn.IsRising());
			m_wallSlideToFallingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is WallSlide && _pawn.IsFalling() && !_pawn.IsWallDetected());
			
			// Coyote
			m_coyoteToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Coyote && _pawn.IsRising());
			m_coyoteToFallingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote && _pawn.IsFalling() && m_coyote.ElapsedTime >= _pawn.CoyoteTime);
		}

		protected override void AddTransitions()
		{
			AddTransition(m_airEntry, m_rising, m_entryToRisingCondition);
			AddTransition(m_airEntry, m_coyote, m_entryToCoyoteCondition);
			
			AddTransition(m_rising, m_apex, m_risingToApexCondition);
			
			AddTransition(m_falling, m_rising, m_fallingToRisingCondition);
			AddTransition(m_falling, m_ledge, m_ledgeCondition);
			AddTransition(m_falling, m_wallSlide, m_wallCondition);
			
			AddTransition(m_apex, m_rising, m_apexToRisingCondition);
			AddTransition(m_apex, m_falling, m_apexToFallingCondition);
			AddTransition(m_apex, m_ledge, m_ledgeCondition);
			
			AddTransition(m_ledge, m_rising, m_ledgeToRisingCondition);
			
			AddTransition(m_wallSlide, m_falling, m_wallSlideToFallingCondition);
			AddTransition(m_wallSlide, m_rising, m_wallSlideToRisingCondition);
			AddTransition(m_wallSlide, m_ledge, m_ledgeCondition);
			
			
			AddTransition(m_coyote, m_falling, m_coyoteToFallingCondition);
			AddTransition(m_coyote, m_rising, m_coyoteToRisingCondition);
			AddTransition(m_coyote, m_ledge, m_ledgeCondition);
			
			AddTransition(m_airExit, m_airEntry, new FuncPredicate(() => false));
		}
	}
}

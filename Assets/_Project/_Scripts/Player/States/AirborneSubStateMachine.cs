using Smash.Player.Components;
using TripleA.StateMachine.FSM;

namespace Smash.Player.States
{
	public sealed class AirborneSubStateMachine : PlayerSubStateMachine
	{
		#region Base States

		private Rising m_rising;
		private Falling m_falling;
		private Coyote m_coyote;
		private Apex m_apex;
		private Ledge m_ledge;
		private WallSlide m_wallSlide;
		
		private FuncPredicate m_anyToRisingCondition;

		private FuncPredicate m_entryToCoyoteCondition;
		
		private FuncPredicate m_risingToApexCondition;
		
		private FuncPredicate m_apexToFallingCondition;
		
		private FuncPredicate m_ledgeCondition;
		private FuncPredicate m_wallCondition;
		
		private FuncPredicate m_wallSlideToFallingCondition;
		
		private FuncPredicate m_coyoteToFallingCondition;

		#endregion Base States

		public AirborneSubStateMachine(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
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
			_stateMachine.SetState(_entry);
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
			ResetBooleans();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.OnDash -= PawnOnOnDash;
			_stateMachine.SetState(_exit);
		}

		#endregion State Machine Methods
		
		protected override void CreateStates()
		{
			base.CreateStates();
			_entry = new AirEntry(_pawn, _graphicsController);
			_exit = new AirExit(_pawn, _graphicsController);
			m_rising = new Rising(_pawn, _graphicsController);
			m_falling = new Falling(_pawn, _graphicsController);
			m_coyote = new Coyote(_pawn, _graphicsController);
			m_apex = new Apex(_pawn, _graphicsController);
			m_ledge = new Ledge(_pawn, _graphicsController);
			m_wallSlide = new WallSlide(_pawn, _graphicsController);
		}
		
		protected override void CreateTransitions()
		{
			m_anyToRisingCondition = new FuncPredicate(() => _stateMachine.CurrentState is not Rising && _pawn.IsRising());
			
			// Entry
			m_entryToCoyoteCondition = new FuncPredicate(() => _pawn.IsFalling());
			
			// Rising
			m_risingToApexCondition = new FuncPredicate(() => _pawn.IsFalling());

			// Independent
			m_ledgeCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote or Falling or Apex or WallSlide && _pawn.IsLedgeGrab());
			m_wallCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Falling && _pawn.IsWallDetected());

			// Apex
			m_apexToFallingCondition = new FuncPredicate(() => 
				_stateMachine.CurrentState is Apex && m_apex.ElapsedTime >= _pawn.ApexTime && _pawn.IsFalling());
			
			
			// WallSlide
			m_wallSlideToFallingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is WallSlide && _pawn.IsFalling() && !_pawn.IsWallDetected());
			
			// Coyote
			m_coyoteToFallingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Coyote && _pawn.IsFalling() && m_coyote.ElapsedTime >= _pawn.CoyoteTime);

			_anyToMainAttackStartCondition = new FuncPredicate(() => IsNotStationaryInAir() && MainAttackHold);
			_anyToMainAttackEndCondition = new FuncPredicate(() => IsNotStationaryInAir() && MainAttackTap);
			_mainAttackStartToEndCondition = new FuncPredicate(() => !MainAttackHold);
			_mainAttackEndToEntryCondition = new FuncPredicate(() => _mainAttackEnd.ElapsedTime >= _mainAttackDuration); // wait for duration to be completed
			
			_anyToSideMainAttackStartCondition = new FuncPredicate(() => IsNotStationaryInAir() && SideMainAttackHold);
			_anyToSideMainAttackEndCondition = new FuncPredicate(() => IsNotStationaryInAir() && SideMainAttackTap);
			_sideMainAttackStartToEndCondition = new FuncPredicate(() => !SideMainAttackHold);
			_sideMainAttackEndToEntryCondition = new FuncPredicate(() => _sideMainAttackEnd.ElapsedTime >= _sideMainAttackDuration); // wait for duration to be completed
			
			_anyToUpMainAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && UpMainAttackHold);
			_anyToUpMainAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && UpMainAttackTap);
			_upMainAttackStartToEndCondition = new FuncPredicate(() => !UpMainAttackHold);
			_upMainAttackEndToEntryCondition = new FuncPredicate(() => true); // wait for duration to be completed
			
			_anyToDownMainAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && DownMainAttackHold);
			_anyToDownMainAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && DownMainAttackTap);
			_downMainAttackStartToEndCondition = new FuncPredicate(() => !DownMainAttackHold);
			_downMainAttackEndToEntryCondition = new FuncPredicate(() => true); // wait for duration to be completed
			
			_anyToSpecialAttackStartCondition = new FuncPredicate(() => IsNotStationaryInAir() && SpecialAttackHold);
			_anyToSpecialAttackEndCondition = new FuncPredicate(() => IsNotStationaryInAir() && SpecialAttackTap);
			_specialAttackStartToEndCondition = new FuncPredicate(() => !SpecialAttackHold);
			_specialAttackEndToEntryCondition = new FuncPredicate(() => _specialAttackEnd.ElapsedTime >= _specialAttackDuration);
			
			_anyToSideSpecialAttackStartCondition = new FuncPredicate(() => IsNotStationaryInAir() && SideSpecialAttackHold);
			_anyToSideSpecialAttackEndCondition = new FuncPredicate(() => IsNotStationaryInAir() && SideSpecialAttackTap);
			_sideSpecialAttackStartToEndCondition = new FuncPredicate(() => !SideSpecialAttackHold);
			_sideSpecialAttackEndToEntryCondition = new FuncPredicate(() => true);
			
			_anyToUpSpecialAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && UpSpecialAttackHold);
			_anyToUpSpecialAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && UpSpecialAttackTap);
			_upSpecialAttackStartToEndCondition = new FuncPredicate(() => !UpSpecialAttackHold);
			_upSpecialAttackEndToEntryCondition = new FuncPredicate(() => true);
			
			_anyToDownSpecialAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && DownSpecialAttackHold);
			_anyToDownSpecialAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && DownSpecialAttackTap);
			_downSpecialAttackStartToEndCondition = new FuncPredicate(() => !DownSpecialAttackHold);
			_downSpecialAttackEndToEntryCondition = new FuncPredicate(() => true);
		}

		protected override void AddTransitions()
		{
			AddAnyTransition(m_rising, m_anyToRisingCondition);
			
			AddTransition(_entry, m_coyote, m_entryToCoyoteCondition);
			
			AddTransition(m_rising, m_apex, m_risingToApexCondition);
			
			AddTransition(m_falling, m_ledge, m_ledgeCondition);
			AddTransition(m_falling, m_wallSlide, m_wallCondition);
			
			AddTransition(m_apex, m_falling, m_apexToFallingCondition);
			AddTransition(m_apex, m_ledge, m_ledgeCondition);
			
			AddTransition(m_wallSlide, m_falling, m_wallSlideToFallingCondition);
			AddTransition(m_wallSlide, m_ledge, m_ledgeCondition);
			
			
			AddTransition(m_coyote, m_falling, m_coyoteToFallingCondition);
			AddTransition(m_coyote, m_ledge, m_ledgeCondition);
			
			AddTransition(_entry, _exit, new FuncPredicate(() => false));
			
			base.AddTransitions();
		}

		/// <summary>
		/// The current state is not stationary in air meaning the player is not on ledge or wall sliding,
		/// The player currently is rising, falling, apex, or coyote
		/// </summary>
		private bool IsNotStationaryInAir()
		{
			return _stateMachine.CurrentState is Rising or Apex or Coyote or Falling;
		}

		private bool IsAnyNonAttackState()
		{
			return _stateMachine.CurrentState is WallSlide or Ledge && IsNotStationaryInAir();
		}
	}
}

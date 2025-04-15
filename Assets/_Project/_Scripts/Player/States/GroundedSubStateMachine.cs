using TripleA.FSM;

namespace Smash.Player.States
{
	public sealed class GroundedSubStateMachine : PlayerSubStateMachine
	{
		#region Base States

		private GroundEntry m_groundEntry;
		private GroundExit m_groundExit;
		private Idle m_idle;
		private Moving m_moving;
		
		private FuncPredicate m_entryToIdleCondition;
		private FuncPredicate m_entryToMovingCondition;
		private FuncPredicate m_idleToMovingCondition;
		private FuncPredicate m_movingToIdleCondition;

		#endregion Base States

		#region Attack States

		#region Main Attack

		private MainAttackStart m_mainAttackStart;
		private MainAttackEnd m_mainAttackEnd;
		
		private FuncPredicate m_anyToMainAttackStartCondition;
		private FuncPredicate m_anyToMainAttackEndCondition;
		private FuncPredicate m_mainAttackStartToEndCondition;
		private FuncPredicate m_mainAttackEndToEntryCondition;

		#endregion Main Attack

		#region Side Main Attack

		private SideMainAttackStart m_sideMainAttackStart;
		private SideMainAttackEnd m_sideMainAttackEnd;
		
		private FuncPredicate m_anyToSideMainAttackStartCondition;
		private FuncPredicate m_anyToSideMainAttackEndCondition;
		private FuncPredicate m_sideMainAttackStartToEndCondition;
		private FuncPredicate m_sideMainAttackEndToEntryCondition;

		#endregion Side Main Attack

		#region Up Main Attack

		private UpMainAttackStart m_upMainAttackStart;
		private UpMainAttackEnd m_upMainAttackEnd;
		
		private FuncPredicate m_anyToUpMainAttackStartCondition;
		private FuncPredicate m_anyToUpMainAttackEndCondition;
		private FuncPredicate m_upMainAttackStartToEndCondition;
		private FuncPredicate m_upMainAttackEndToEntryCondition;

		#endregion Up Main Attack

		#region Down Main Attack

		private DownMainAttackStart m_downMainAttackStart;
		private DownMainAttackEnd m_downMainAttackEnd;
		
		private FuncPredicate m_anyToDownMainAttackStartCondition;
		private FuncPredicate m_anyToDownMainAttackEndCondition;
		private FuncPredicate m_downMainAttackStartToEndCondition;
		private FuncPredicate m_downMainAttackEndToEntryCondition;

		#endregion Down Main Attack

		#endregion Attack States
		
		public GroundedSubStateMachine(PlayerPawn pawn) : base(pawn)
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
			_pawn.OnDash += PawnOnOnDash;
			_pawn.CurrentState = this;
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
			_pawn.OnDash -= PawnOnOnDash;
			_stateMachine.SetState(m_groundExit);
		}

		#endregion State Machine Methods

		protected override void CreateStates()
		{
			m_groundEntry = new GroundEntry(_pawn);
			m_groundExit = new GroundExit(_pawn);
			m_idle = new Idle(_pawn);
			m_moving = new Moving(_pawn);
			
			m_mainAttackStart = new MainAttackStart(_pawn);
			m_mainAttackEnd = new MainAttackEnd(_pawn);
			
			m_sideMainAttackStart = new SideMainAttackStart(_pawn);
			m_sideMainAttackEnd = new SideMainAttackEnd(_pawn);
			
			m_upMainAttackStart = new UpMainAttackStart(_pawn);
			m_upMainAttackEnd = new UpMainAttackEnd(_pawn);
			
			m_downMainAttackStart = new DownMainAttackStart(_pawn);
			m_downMainAttackEnd = new DownMainAttackEnd(_pawn);
		}

		protected override void CreateTransitions()
		{
			m_entryToIdleCondition = new FuncPredicate(() => !_pawn.IsMoving());
			m_entryToMovingCondition = new FuncPredicate(() => _pawn.IsMoving());
			
			m_idleToMovingCondition = new FuncPredicate(() => _pawn.IsMoving());
			
			m_movingToIdleCondition = new FuncPredicate(() => !_pawn.IsMoving());

			m_anyToMainAttackStartCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle or Moving && MainAttackHold);
			m_anyToMainAttackEndCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle or Moving && MainAttackTap);
			m_mainAttackStartToEndCondition = new FuncPredicate(() => !MainAttackHold);
			m_mainAttackEndToEntryCondition = new FuncPredicate(() => true); // wait for duration to be completed
			
			m_anyToSideMainAttackStartCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle or Moving && SideMainAttackHold);
			m_anyToSideMainAttackEndCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle or Moving && SideMainAttackTap);
			m_sideMainAttackStartToEndCondition = new FuncPredicate(() => !SideMainAttackHold);
			m_sideMainAttackEndToEntryCondition = new FuncPredicate(() => true); // wait for duration to be completed
			
			m_anyToUpMainAttackStartCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle or Moving && UpMainAttackHold);
			m_anyToUpMainAttackEndCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle or Moving && UpMainAttackTap);
			m_upMainAttackStartToEndCondition = new FuncPredicate(() => !UpMainAttackHold);
			m_upMainAttackEndToEntryCondition = new FuncPredicate(() => true); // wait for duration to be completed
			
			m_anyToDownMainAttackStartCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle or Moving && DownMainAttackHold);
			m_anyToDownMainAttackEndCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle or Moving && DownMainAttackTap);
			m_downMainAttackStartToEndCondition = new FuncPredicate(() => !DownMainAttackHold);
			m_downMainAttackEndToEntryCondition = new FuncPredicate(() => true); // wait for duration to be completed
			
			/*m_dashToIdleCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Dash && !_pawn.IsMoving() && m_dash.IsFinished);
			m_dashToMovingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Dash && _pawn.IsMoving() && m_dash.IsFinished);*/
		}

		protected override void AddTransitions()
		{
			AddTransition(m_groundEntry, m_idle, m_entryToIdleCondition);
			AddTransition(m_groundEntry, m_moving, m_entryToMovingCondition);
			
			AddTransition(m_idle, m_moving, m_idleToMovingCondition);
			
			AddTransition(m_moving, m_idle, m_movingToIdleCondition);
			
			AddAnyTransition(m_mainAttackStart, m_anyToMainAttackStartCondition);
			AddAnyTransition(m_mainAttackEnd, m_anyToMainAttackEndCondition);
			AddTransition(m_mainAttackStart, m_mainAttackEnd, m_mainAttackStartToEndCondition);
			AddTransition(m_mainAttackEnd, m_groundEntry, m_mainAttackEndToEntryCondition);
			
			AddAnyTransition(m_sideMainAttackStart, m_anyToSideMainAttackStartCondition);
			AddAnyTransition(m_sideMainAttackEnd, m_anyToSideMainAttackEndCondition);
			AddTransition(m_sideMainAttackStart, m_sideMainAttackEnd, m_sideMainAttackStartToEndCondition);
			AddTransition(m_sideMainAttackEnd, m_groundEntry, m_sideMainAttackEndToEntryCondition);
			
			AddAnyTransition(m_upMainAttackStart, m_anyToUpMainAttackStartCondition);
			AddAnyTransition(m_upMainAttackEnd, m_anyToUpMainAttackEndCondition);
			AddTransition(m_upMainAttackStart, m_upMainAttackEnd, m_upMainAttackStartToEndCondition);
			AddTransition(m_upMainAttackEnd, m_groundEntry, m_upMainAttackEndToEntryCondition);
			
			AddAnyTransition(m_downMainAttackStart, m_anyToDownMainAttackStartCondition);
			AddAnyTransition(m_downMainAttackEnd, m_anyToDownMainAttackEndCondition);
			AddTransition(m_downMainAttackStart, m_downMainAttackEnd, m_downMainAttackStartToEndCondition);
			AddTransition(m_downMainAttackEnd, m_groundEntry, m_downMainAttackEndToEntryCondition);
			
			AddTransition(m_groundExit, m_groundEntry, new FuncPredicate(() => false));
		}
	}
}

using Smash.Player.Components;
using TripleA.StateMachine.FSM;
namespace Smash.Player.States
{
	public sealed class GroundedSubStateMachine : PlayerSubStateMachine
	{
		#region Base States

		private Idle m_idle;
		private Moving m_moving;
		
		private FuncPredicate m_entryToIdleCondition;
		private FuncPredicate m_entryToMovingCondition;
		private FuncPredicate m_idleToMovingCondition;
		private FuncPredicate m_movingToIdleCondition;
		
		#endregion Base States
		
		public GroundedSubStateMachine(CharacterPawn pawn, PlayerGraphicsController graphicsController,
			float mainAttackDuration, float sideMainAttackDuration, float upMainAttackDuration,
			float specialAttackDuration, float upSpecialAttackDuration) : 
			base(pawn, graphicsController, mainAttackDuration, sideMainAttackDuration, upMainAttackDuration, 
				specialAttackDuration, upSpecialAttackDuration)
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
			
			_entry = new GroundEntry(_pawn, _graphicsController);
			_exit = new GroundExit(_pawn, _graphicsController);
			m_idle = new Idle(_pawn, _graphicsController);
			m_moving = new Moving(_pawn, _graphicsController);
		}

		protected override void CreateTransitions()
		{
			m_entryToIdleCondition = new FuncPredicate(() => !_pawn.IsMoving());
			m_entryToMovingCondition = new FuncPredicate(() => _pawn.IsMoving());	
			
			m_idleToMovingCondition = new FuncPredicate(() => _pawn.IsMoving());
			
			m_movingToIdleCondition = new FuncPredicate(() => !_pawn.IsMoving());

			_anyToMainAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && MainAttackHold);
			_anyToMainAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && MainAttackTap);
			
			_anyToSideMainAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && SideMainAttackHold);
			_anyToSideMainAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && SideMainAttackTap);
			
			_anyToUpMainAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && UpMainAttackHold);
			_anyToUpMainAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && UpMainAttackTap);
			
			_anyToDownMainAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && DownMainAttackHold);
			_anyToDownMainAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && DownMainAttackTap);
			
			_anyToSpecialAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && SpecialAttackHold);
			_anyToSpecialAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && SpecialAttackTap);
			
			_anyToSideSpecialAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && SideSpecialAttackHold);
			_anyToSideSpecialAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && SideSpecialAttackTap);
			
			_anyToUpSpecialAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && UpSpecialAttackHold);
			_anyToUpSpecialAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && UpSpecialAttackTap);
			
			_anyToDownSpecialAttackStartCondition = new FuncPredicate(() => IsAnyNonAttackState() && DownSpecialAttackHold);
			_anyToDownSpecialAttackEndCondition = new FuncPredicate(() => IsAnyNonAttackState() && DownSpecialAttackTap);
			
			_anyToKnockBackCondition = new FuncPredicate(() => _stateMachine.CurrentState is not 
				(TossUpStart or TossUpEnd or KnockBack) && _pawn.IsKnockedBack() && !_pawn.IsTossedUp());
			_knockBackToEntryCondition = new FuncPredicate(() => !_pawn.IsKnockedBack());
			
			base.CreateTransitions();
			
			/*m_dashToIdleCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Dash && !_pawn.IsMoving() && m_dash.IsFinished);
			m_dashToMovingCondition = new FuncPredicate(() =>
				_stateMachine.CurrentState is Dash && _pawn.IsMoving() && m_dash.IsFinished);*/
		}

		protected override void AddTransitions()
		{
			AddTransition(_entry, m_idle, m_entryToIdleCondition);
			AddTransition(_entry, m_moving, m_entryToMovingCondition);
			
			AddTransition(m_idle, m_moving, m_idleToMovingCondition);
			
			AddTransition(m_moving, m_idle, m_movingToIdleCondition);
			
			AddTransition(_exit, _entry, new FuncPredicate(() => false));
			
			base.AddTransitions();
		}

		private bool IsAnyNonAttackState()
		{
			return _stateMachine.CurrentState is Idle or Moving;
		}
	}
}

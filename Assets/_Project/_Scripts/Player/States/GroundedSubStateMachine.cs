using TripleA.FSM;

namespace Smash.Player.States
{
	public sealed class GroundedSubStateMachine : PlayerSubStateMachine
	{
		private GroundEntry m_groundEntry;
		private GroundExit m_groundExit;
		private Idle m_idle;
		private Moving m_moving;

		private bool m_isExiting;
		
		private readonly FuncPredicate m_entryToIdleCondition;
		private readonly FuncPredicate m_entryToMovingCondition;
		private readonly FuncPredicate m_idleToMovingCondition;
		private readonly FuncPredicate m_movingToIdleCondition;
		
		public GroundedSubStateMachine(PlayerController controller) : base(controller)
		{
			_stateMachine = new StateMachine();

			CreateStates();
			
			m_entryToIdleCondition = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && !_controller.IsMoving());
			m_entryToMovingCondition = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _controller.IsMoving());
			m_idleToMovingCondition = new FuncPredicate(() => _stateMachine.CurrentState is Idle && _controller.IsMoving());
			m_movingToIdleCondition = new FuncPredicate(() => _stateMachine.CurrentState is Moving && !_controller.IsMoving());
			
			CreateAndAddTransitions();
		}

		#region State Machine Methods

		public override void OnEnter()
		{
			base.OnEnter();
			
			m_isExiting = false;
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
			m_isExiting = true;
		}

		#endregion State Machine Methods

		protected override void CreateStates()
		{
			m_groundEntry = new GroundEntry(_controller);
			m_groundExit = new GroundExit(_controller);
			m_idle = new Idle(_controller);
			m_moving = new Moving(_controller);
		}

		protected override void CreateAndAddTransitions()
		{
			AddTransition(m_groundEntry, m_idle, m_entryToIdleCondition);
			AddTransition(m_groundEntry, m_moving, m_entryToMovingCondition);
			AddTransition(m_idle, m_moving, m_idleToMovingCondition);
			AddTransition(m_moving, m_idle, m_movingToIdleCondition);
			
			// Exit
			AddAnyTransition(m_groundExit, new FuncPredicate(() => m_isExiting));
		}
	}
}
using TripleA.FSM;

namespace Smash.Player.States
{
	public sealed class AirborneSubStateMachine : PlayerSubStateMachine
	{
		private AirEntry m_airEntry;
		private AirExit m_airExit;
		private Rising m_rising;
		private Falling m_falling;

		private bool m_isExiting;

		private readonly FuncPredicate m_entryToRising;
		private readonly FuncPredicate m_entryToFalling;
		private readonly FuncPredicate m_risingToFalling;
		private readonly FuncPredicate m_fallingToRising;
		
		public AirborneSubStateMachine(PlayerController controller) : base(controller)
		{
			_stateMachine = new StateMachine();
			
			CreateStates();
			
			m_entryToRising = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _controller.IsRising());
			m_entryToFalling = new FuncPredicate(() => _stateMachine.CurrentState is AirEntry && _controller.IsFalling());
			m_risingToFalling = new FuncPredicate(() => _stateMachine.CurrentState is Rising && _controller.IsFalling());
			m_fallingToRising = new FuncPredicate(() => _stateMachine.CurrentState is Falling && _controller.IsRising());
			
			CreateAndAddTransitions();
		}

		#region State Machine Methods

		public override void OnEnter()
		{
			base.OnEnter();
			
			m_isExiting = false;
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
			m_isExiting = true;
		}

		#endregion State Machine Methods

		protected override void CreateStates()
		{
			m_airEntry = new AirEntry(_controller);
			m_airExit = new AirExit(_controller);
			m_rising = new Rising(_controller);
			m_falling = new Falling(_controller);
		}

		protected override void CreateAndAddTransitions()
		{
			// Entry
			AddTransition(m_airEntry, m_rising, m_entryToRising);
			AddTransition(m_airEntry, m_falling, m_entryToFalling);
			AddTransition(m_rising, m_falling, m_risingToFalling);
			AddTransition(m_falling, m_rising, m_fallingToRising);
			
			// Exit
			AddAnyTransition(m_airExit, new FuncPredicate(() => m_isExiting));
		}
	}
}
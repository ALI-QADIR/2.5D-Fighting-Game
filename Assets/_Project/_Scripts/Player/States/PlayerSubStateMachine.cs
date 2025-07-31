using Smash.Player.Components;
using TripleA.StateMachine.FSM;

namespace Smash.Player.States
{
	public abstract class PlayerSubStateMachine : PlayerBaseState
	{
		protected StateMachine _stateMachine;
		protected bool _dashPressed;

		private readonly float _mainAttackDuration;
		private readonly float _sideMainAttackDuration;
		private readonly float _upMainAttackDuration;
		private readonly float _specialAttackDuration;
		private readonly float _upSpecialAttackDuration;

		#region Transition Booleans

		public bool MainAttackTap { get; set; }
		public bool MainAttackHold { get; set; }
		public bool SideMainAttackTap { get; set; }
		public bool SideMainAttackHold { get; set; }
		public bool UpMainAttackTap { get; set; }
		public bool UpMainAttackHold { get; set; }
		public bool DownMainAttackTap { get; set; }
		public bool DownMainAttackHold { get; set; }
		
		public bool SpecialAttackTap { get; set; }
		public bool SpecialAttackHold { get; set; }
		public bool SideSpecialAttackTap { get; set; }
		public bool SideSpecialAttackHold { get; set; }
		public bool UpSpecialAttackTap { get; set; }
		public bool UpSpecialAttackHold { get; set; }
		public bool DownSpecialAttackTap { get; set; }
		public bool DownSpecialAttackHold { get; set; }

		#endregion Transition Booleans

		protected PlayerBaseState _entry;
		protected PlayerBaseState _exit;
		
		#region Attack States

		#region Main Attack

		private MainAttackStart m_mainAttackStart;
		private MainAttackEnd m_mainAttackEnd;

		protected FuncPredicate _anyToMainAttackStartCondition;
		protected FuncPredicate _anyToMainAttackEndCondition;
		private FuncPredicate m_mainAttackStartToEndCondition;
		private FuncPredicate m_mainAttackEndToEntryCondition;

		#endregion Main Attack

		#region Side Main Attack

		private SideMainAttackStart m_sideMainAttackStart;
		private SideMainAttackEnd m_sideMainAttackEnd;

		protected FuncPredicate _anyToSideMainAttackStartCondition;
		protected FuncPredicate _anyToSideMainAttackEndCondition;
		private FuncPredicate m_sideMainAttackStartToEndCondition;
		private FuncPredicate m_sideMainAttackEndToEntryCondition;

		#endregion Side Main Attack

		#region Up Main Attack

		private UpMainAttackStart m_upMainAttackStart;
		private UpMainAttackEnd m_upMainAttackEnd;

		protected FuncPredicate _anyToUpMainAttackStartCondition;
		protected FuncPredicate _anyToUpMainAttackEndCondition;
		private FuncPredicate m_upMainAttackStartToEndCondition;
		private FuncPredicate m_upMainAttackEndToEntryCondition;

		#endregion Up Main Attack

		#region Down Main Attack

		private DownMainAttackStart m_downMainAttackStart;
		private DownMainAttackEnd m_downMainAttackEnd;

		protected FuncPredicate _anyToDownMainAttackStartCondition;
		protected FuncPredicate _anyToDownMainAttackEndCondition;
		private FuncPredicate m_downMainAttackStartToEndCondition;
		private FuncPredicate m_downMainAttackEndToEntryCondition;

		#endregion Down Main Attack

		#region Special Attack

		private SpecialAttackStart m_specialAttackStart;
		private SpecialAttackEnd m_specialAttackEnd;

		protected FuncPredicate _anyToSpecialAttackStartCondition;
		protected FuncPredicate _anyToSpecialAttackEndCondition;
		private FuncPredicate m_specialAttackStartToEndCondition;
		private FuncPredicate m_specialAttackEndToEntryCondition;

		#endregion

		#region Side Special Attack

		private SideSpecialAttackStart m_sideSpecialAttackStart;
		private SideSpecialAttackEnd m_sideSpecialAttackEnd;

		protected FuncPredicate _anyToSideSpecialAttackStartCondition;
		protected FuncPredicate _anyToSideSpecialAttackEndCondition;
		private FuncPredicate m_sideSpecialAttackStartToEndCondition;
		private FuncPredicate m_sideSpecialAttackEndToEntryCondition;

		#endregion

		#region Up Special Attack

		private UpSpecialAttackStart m_upSpecialAttackStart;
		private UpSpecialAttackEnd m_upSpecialAttackEnd;

		protected FuncPredicate _anyToUpSpecialAttackStartCondition;
		protected FuncPredicate _anyToUpSpecialAttackEndCondition;
		private FuncPredicate m_upSpecialAttackStartToEndCondition;
		private FuncPredicate m_upSpecialAttackEndToEntryCondition;

		#endregion

		#region Down Special Attack

		private DownSpecialAttackStart m_downSpecialAttackStart;
		private DownSpecialAttackEnd m_downSpecialAttackEnd;

		protected FuncPredicate _anyToDownSpecialAttackStartCondition;
		protected FuncPredicate _anyToDownSpecialAttackEndCondition;
		private FuncPredicate m_downSpecialAttackStartToEndCondition;
		private FuncPredicate m_downSpecialAttackEndToEntryCondition;

		#endregion

		#endregion Attack States
		
		#region Hurt States

		private KnockBack m_knockBack;
		protected FuncPredicate _anyToKnockBackCondition;
		protected FuncPredicate _knockBackToEntryCondition;

		private TossUpStart m_tossUpStart;
		private TossUpEnd m_tossUpEnd;
		private const float k_Toss_Up_Sanity_Check = 0.2f;
		private FuncPredicate m_anyToTossUpStartCondition;
		private FuncPredicate m_tossUpStartToEndCondition;
		private FuncPredicate m_tossUpEndToEntryCondition;

		private Stun m_stun;
		private FuncPredicate m_anyToStunCondition;
		private FuncPredicate m_stunToEntryCondition;
		public float stunDuration;
		
		#endregion Hurt States

		protected PlayerSubStateMachine(CharacterPawn pawn, PlayerGraphicsController graphicsController,
			float mainAttackDuration, float sideMainAttackDuration, float upMainAttackDuration, 
			float specialAttackDuration, float upSpecialAttackDuration) : 
			base(pawn, graphicsController)
		{
			_mainAttackDuration = mainAttackDuration;
			_sideMainAttackDuration = sideMainAttackDuration;
			_upMainAttackDuration = upMainAttackDuration;
			_specialAttackDuration = specialAttackDuration;
			_upSpecialAttackDuration = upSpecialAttackDuration;
		}

		protected virtual void CreateStates()
		{
			m_mainAttackStart = new MainAttackStart(_pawn, _graphicsController);
			m_mainAttackEnd = new MainAttackEnd(_pawn, _graphicsController);
			
			m_sideMainAttackStart = new SideMainAttackStart(_pawn, _graphicsController);
			m_sideMainAttackEnd = new SideMainAttackEnd(_pawn, _graphicsController);
			
			m_upMainAttackStart = new UpMainAttackStart(_pawn, _graphicsController);
			m_upMainAttackEnd = new UpMainAttackEnd(_pawn, _graphicsController);
			
			m_downMainAttackStart = new DownMainAttackStart(_pawn, _graphicsController);
			m_downMainAttackEnd = new DownMainAttackEnd(_pawn, _graphicsController);
			
			m_specialAttackStart = new SpecialAttackStart(_pawn, _graphicsController);
			m_specialAttackEnd = new SpecialAttackEnd(_pawn, _graphicsController);
			
			m_sideSpecialAttackStart = new SideSpecialAttackStart(_pawn, _graphicsController);
			m_sideSpecialAttackEnd = new SideSpecialAttackEnd(_pawn, _graphicsController);
			
			m_upSpecialAttackStart = new UpSpecialAttackStart(_pawn, _graphicsController);
			m_upSpecialAttackEnd = new UpSpecialAttackEnd(_pawn, _graphicsController);
			
			m_downSpecialAttackStart = new DownSpecialAttackStart(_pawn, _graphicsController);
			m_downSpecialAttackEnd = new DownSpecialAttackEnd(_pawn, _graphicsController);
			
			m_knockBack = new KnockBack(_pawn, _graphicsController);
			
			m_tossUpStart = new TossUpStart(_pawn, _graphicsController);
			m_tossUpEnd = new TossUpEnd(_pawn, _graphicsController);
			
			m_stun = new Stun(_pawn, _graphicsController);
		}

		protected virtual void CreateTransitions()
		{
			m_mainAttackStartToEndCondition = new FuncPredicate(() => !MainAttackHold);
			m_mainAttackEndToEntryCondition = new FuncPredicate(() => m_mainAttackEnd.ElapsedTime >= _mainAttackDuration);
			
			m_sideMainAttackStartToEndCondition = new FuncPredicate(() => !SideMainAttackHold);
			m_sideMainAttackEndToEntryCondition = new FuncPredicate(() => m_sideMainAttackEnd.ElapsedTime >= _sideMainAttackDuration);
			
			m_upMainAttackStartToEndCondition = new FuncPredicate(() => !UpMainAttackHold);
			m_upMainAttackEndToEntryCondition = new FuncPredicate(() => m_upMainAttackEnd.ElapsedTime >= _upMainAttackDuration);
			
			m_downMainAttackStartToEndCondition = new FuncPredicate(() => !DownMainAttackHold);
			m_downMainAttackEndToEntryCondition = new FuncPredicate(() => true); // wait for duration to be completed
			
			m_specialAttackStartToEndCondition = new FuncPredicate(() => !SpecialAttackHold);
			m_specialAttackEndToEntryCondition = new FuncPredicate(() => m_specialAttackEnd.ElapsedTime >= _specialAttackDuration);
			
			m_sideSpecialAttackStartToEndCondition = new FuncPredicate(() => !SideSpecialAttackHold);
			m_sideSpecialAttackEndToEntryCondition = new FuncPredicate(() => true);
			
			m_upSpecialAttackStartToEndCondition = new FuncPredicate(() => !UpSpecialAttackHold);
			m_upSpecialAttackEndToEntryCondition = new FuncPredicate(() => m_upSpecialAttackEnd.ElapsedTime >= _upSpecialAttackDuration);
			
			m_downSpecialAttackStartToEndCondition = new FuncPredicate(() => !DownSpecialAttackHold);
			m_downSpecialAttackEndToEntryCondition = new FuncPredicate(() => true);
			
			m_anyToTossUpStartCondition = new FuncPredicate(() => _stateMachine.CurrentState is not (TossUpStart or TossUpEnd) && _pawn.IsTossedUp());
			m_tossUpStartToEndCondition = new FuncPredicate(() => _pawn.IsGrounded() && m_tossUpStart.ElapseTime > k_Toss_Up_Sanity_Check);
			m_tossUpEndToEntryCondition = new FuncPredicate(() => !_pawn.IsTossedUp());
			
			m_anyToStunCondition = new FuncPredicate(() => _stateMachine.CurrentState is not Stun && _pawn.IsStunned);
			m_stunToEntryCondition = new FuncPredicate(() => !_pawn.IsStunned || m_stun.ElapsedTime >= stunDuration);
		}

		protected virtual void AddTransitions()
		{
			AddAnyTransition(m_mainAttackStart, _anyToMainAttackStartCondition);
			AddAnyTransition(m_mainAttackEnd, _anyToMainAttackEndCondition);
			AddTransition(m_mainAttackStart, m_mainAttackEnd, m_mainAttackStartToEndCondition);
			AddTransition(m_mainAttackEnd, _entry, m_mainAttackEndToEntryCondition);
			
			AddAnyTransition(m_sideMainAttackStart, _anyToSideMainAttackStartCondition);
			AddAnyTransition(m_sideMainAttackEnd, _anyToSideMainAttackEndCondition);
			AddTransition(m_sideMainAttackStart, m_sideMainAttackEnd, m_sideMainAttackStartToEndCondition);
			AddTransition(m_sideMainAttackEnd, _entry, m_sideMainAttackEndToEntryCondition);
			
			AddAnyTransition(m_upMainAttackStart, _anyToUpMainAttackStartCondition);
			AddAnyTransition(m_upMainAttackEnd, _anyToUpMainAttackEndCondition);
			AddTransition(m_upMainAttackStart, m_upMainAttackEnd, m_upMainAttackStartToEndCondition);
			AddTransition(m_upMainAttackEnd, _entry, m_upMainAttackEndToEntryCondition);
			
			AddAnyTransition(m_downMainAttackStart, _anyToDownMainAttackStartCondition);
			AddAnyTransition(m_downMainAttackEnd, _anyToDownMainAttackEndCondition);
			AddTransition(m_downMainAttackStart, m_downMainAttackEnd, m_downMainAttackStartToEndCondition);
			AddTransition(m_downMainAttackEnd, _entry, m_downMainAttackEndToEntryCondition);
			
			AddAnyTransition(m_specialAttackStart, _anyToSpecialAttackStartCondition);
			AddAnyTransition(m_specialAttackEnd, _anyToSpecialAttackEndCondition);
			AddTransition(m_specialAttackStart, m_specialAttackEnd, m_specialAttackStartToEndCondition);
			AddTransition(m_specialAttackEnd, _entry, m_specialAttackEndToEntryCondition);
			
			AddAnyTransition(m_sideSpecialAttackStart, _anyToSideSpecialAttackStartCondition);
			AddAnyTransition(m_sideSpecialAttackEnd, _anyToSideSpecialAttackEndCondition);
			AddTransition(m_sideSpecialAttackStart, m_sideSpecialAttackEnd, m_sideSpecialAttackStartToEndCondition);
			AddTransition(m_sideSpecialAttackEnd, _entry, m_sideSpecialAttackEndToEntryCondition);
			
			AddAnyTransition(m_upSpecialAttackStart, _anyToUpSpecialAttackStartCondition);
			AddAnyTransition(m_upSpecialAttackEnd, _anyToUpSpecialAttackEndCondition);
			AddTransition(m_upSpecialAttackStart, m_upSpecialAttackEnd, m_upSpecialAttackStartToEndCondition);
			AddTransition(m_upSpecialAttackEnd, _entry, m_upSpecialAttackEndToEntryCondition);
			
			AddAnyTransition(m_downSpecialAttackStart, _anyToDownSpecialAttackStartCondition);
			AddAnyTransition(m_downSpecialAttackEnd, _anyToDownSpecialAttackEndCondition);
			AddTransition(m_downSpecialAttackStart, m_downSpecialAttackEnd, m_downSpecialAttackStartToEndCondition);
			AddTransition(m_downSpecialAttackEnd, _entry, m_downSpecialAttackEndToEntryCondition);
			
			AddAnyTransition(m_knockBack, _anyToKnockBackCondition);
			AddTransition(m_knockBack, _entry, _knockBackToEntryCondition);
			
			AddAnyTransition(m_tossUpStart, m_anyToTossUpStartCondition);
			AddTransition(m_tossUpStart, m_tossUpEnd, m_tossUpStartToEndCondition);
			AddTransition(m_tossUpEnd, _entry, m_tossUpEndToEntryCondition);
			
			AddAnyTransition(m_stun, m_anyToStunCondition);
			AddTransition(m_stun, _entry, m_stunToEntryCondition);
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentStateMachine = this;
		}


		protected bool DashPredicate<T>()
		{
			bool flag = _stateMachine.CurrentState is T && _dashPressed;
			_dashPressed = false;
			return flag;
		}

		protected void PawnOnOnDash(bool value)
		{
			_dashPressed = value;
		}
		
		protected void AddTransition(IState from, IState to, IPredicate condition) =>
			_stateMachine.AddTransition(from, to, condition);
		
		protected void AddAnyTransition(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

		protected void ResetBooleans()
		{
			MainAttackHold = false;
			MainAttackTap = false;
			SideMainAttackHold = false;
			SideMainAttackTap = false;
			UpMainAttackHold = false;
			UpMainAttackTap = false;
			DownMainAttackHold = false;
			DownMainAttackTap = false;
			
			SpecialAttackHold = false;
			SpecialAttackTap = false;
			SideSpecialAttackTap = false;
			SideSpecialAttackHold = false;
			UpSpecialAttackHold = false;
			UpSpecialAttackTap = false;
			DownSpecialAttackHold = false;
			DownSpecialAttackTap = false;
		}
	}
}

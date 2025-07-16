using Smash.Player.Components;
using TripleA.StateMachine.FSM;

namespace Smash.Player.States
{
	public abstract class PlayerSubStateMachine : PlayerBaseState
	{
		protected StateMachine _stateMachine;
		protected bool _dashPressed;

		protected readonly float _mainAttackDuration;
		protected readonly float _specialAttackDuration;
		protected readonly float _sideMainAttackDuration;
		protected readonly float _upMainAttackDuration;

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

		protected MainAttackStart _mainAttackStart;
		protected MainAttackEnd _mainAttackEnd;

		protected FuncPredicate _anyToMainAttackStartCondition;
		protected FuncPredicate _anyToMainAttackEndCondition;
		protected FuncPredicate _mainAttackStartToEndCondition;
		protected FuncPredicate _mainAttackEndToEntryCondition;

		#endregion Main Attack

		#region Side Main Attack

		protected SideMainAttackStart _sideMainAttackStart;
		protected SideMainAttackEnd _sideMainAttackEnd;

		protected FuncPredicate _anyToSideMainAttackStartCondition;
		protected FuncPredicate _anyToSideMainAttackEndCondition;
		protected FuncPredicate _sideMainAttackStartToEndCondition;
		protected FuncPredicate _sideMainAttackEndToEntryCondition;

		#endregion Side Main Attack

		#region Up Main Attack

		protected UpMainAttackStart _upMainAttackStart;
		protected UpMainAttackEnd _upMainAttackEnd;

		protected FuncPredicate _anyToUpMainAttackStartCondition;
		protected FuncPredicate _anyToUpMainAttackEndCondition;
		protected FuncPredicate _upMainAttackStartToEndCondition;
		protected FuncPredicate _upMainAttackEndToEntryCondition;

		#endregion Up Main Attack

		#region Down Main Attack

		protected DownMainAttackStart _downMainAttackStart;
		protected DownMainAttackEnd _downMainAttackEnd;

		protected FuncPredicate _anyToDownMainAttackStartCondition;
		protected FuncPredicate _anyToDownMainAttackEndCondition;
		protected FuncPredicate _downMainAttackStartToEndCondition;
		protected FuncPredicate _downMainAttackEndToEntryCondition;

		#endregion Down Main Attack

		#region Special Attack

		protected SpecialAttackStart _specialAttackStart;
		protected SpecialAttackEnd _specialAttackEnd;

		protected FuncPredicate _anyToSpecialAttackStartCondition;
		protected FuncPredicate _anyToSpecialAttackEndCondition;
		protected FuncPredicate _specialAttackStartToEndCondition;
		protected FuncPredicate _specialAttackEndToEntryCondition;

		#endregion

		#region Side Special Attack

		protected SideSpecialAttackStart _sideSpecialAttackStart;
		protected SideSpecialAttackEnd _sideSpecialAttackEnd;

		protected FuncPredicate _anyToSideSpecialAttackStartCondition;
		protected FuncPredicate _anyToSideSpecialAttackEndCondition;
		protected FuncPredicate _sideSpecialAttackStartToEndCondition;
		protected FuncPredicate _sideSpecialAttackEndToEntryCondition;

		#endregion

		#region Up Special Attack

		protected UpSpecialAttackStart _upSpecialAttackStart;
		protected UpSpecialAttackEnd _upSpecialAttackEnd;

		protected FuncPredicate _anyToUpSpecialAttackStartCondition;
		protected FuncPredicate _anyToUpSpecialAttackEndCondition;
		protected FuncPredicate _upSpecialAttackStartToEndCondition;
		protected FuncPredicate _upSpecialAttackEndToEntryCondition;

		#endregion

		#region Down Special Attack

		protected DownSpecialAttackStart _downSpecialAttackStart;
		protected DownSpecialAttackEnd _downSpecialAttackEnd;

		protected FuncPredicate _anyToDownSpecialAttackStartCondition;
		protected FuncPredicate _anyToDownSpecialAttackEndCondition;
		protected FuncPredicate _downSpecialAttackStartToEndCondition;
		protected FuncPredicate _downSpecialAttackEndToEntryCondition;

		#endregion

		#endregion Attack States

		protected PlayerSubStateMachine(CharacterPawn pawn, PlayerGraphicsController graphicsController,
			float mainAttackDuration, float sideMainAttackDuration, float upMainAttackDuration, float specialAttackDuration) : base(pawn,
			graphicsController)
		{
			_mainAttackDuration = mainAttackDuration;
			_sideMainAttackDuration = sideMainAttackDuration;
			_specialAttackDuration = specialAttackDuration;
			_upMainAttackDuration = upMainAttackDuration;
		}

		protected virtual void CreateStates()
		{
			_mainAttackStart = new MainAttackStart(_pawn, _graphicsController);
			_mainAttackEnd = new MainAttackEnd(_pawn, _graphicsController);
			
			_sideMainAttackStart = new SideMainAttackStart(_pawn, _graphicsController);
			_sideMainAttackEnd = new SideMainAttackEnd(_pawn, _graphicsController);
			
			_upMainAttackStart = new UpMainAttackStart(_pawn, _graphicsController);
			_upMainAttackEnd = new UpMainAttackEnd(_pawn, _graphicsController);
			
			_downMainAttackStart = new DownMainAttackStart(_pawn, _graphicsController);
			_downMainAttackEnd = new DownMainAttackEnd(_pawn, _graphicsController);
			
			_specialAttackStart = new SpecialAttackStart(_pawn, _graphicsController);
			_specialAttackEnd = new SpecialAttackEnd(_pawn, _graphicsController);
			
			_sideSpecialAttackStart = new SideSpecialAttackStart(_pawn, _graphicsController);
			_sideSpecialAttackEnd = new SideSpecialAttackEnd(_pawn, _graphicsController);
			
			_upSpecialAttackStart = new UpSpecialAttackStart(_pawn, _graphicsController);
			_upSpecialAttackEnd = new UpSpecialAttackEnd(_pawn, _graphicsController);
			
			_downSpecialAttackStart = new DownSpecialAttackStart(_pawn, _graphicsController);
			_downSpecialAttackEnd = new DownSpecialAttackEnd(_pawn, _graphicsController);
		}
		
		protected virtual void CreateTransitions() {}

		protected virtual void AddTransitions()
		{
			AddAnyTransition(_mainAttackStart, _anyToMainAttackStartCondition);
			AddAnyTransition(_mainAttackEnd, _anyToMainAttackEndCondition);
			AddTransition(_mainAttackStart, _mainAttackEnd, _mainAttackStartToEndCondition);
			AddTransition(_mainAttackEnd, _entry, _mainAttackEndToEntryCondition);
			
			AddAnyTransition(_sideMainAttackStart, _anyToSideMainAttackStartCondition);
			AddAnyTransition(_sideMainAttackEnd, _anyToSideMainAttackEndCondition);
			AddTransition(_sideMainAttackStart, _sideMainAttackEnd, _sideMainAttackStartToEndCondition);
			AddTransition(_sideMainAttackEnd, _entry, _sideMainAttackEndToEntryCondition);
			
			AddAnyTransition(_upMainAttackStart, _anyToUpMainAttackStartCondition);
			AddAnyTransition(_upMainAttackEnd, _anyToUpMainAttackEndCondition);
			AddTransition(_upMainAttackStart, _upMainAttackEnd, _upMainAttackStartToEndCondition);
			AddTransition(_upMainAttackEnd, _entry, _upMainAttackEndToEntryCondition);
			
			AddAnyTransition(_downMainAttackStart, _anyToDownMainAttackStartCondition);
			AddAnyTransition(_downMainAttackEnd, _anyToDownMainAttackEndCondition);
			AddTransition(_downMainAttackStart, _downMainAttackEnd, _downMainAttackStartToEndCondition);
			AddTransition(_downMainAttackEnd, _entry, _downMainAttackEndToEntryCondition);
			
			AddAnyTransition(_specialAttackStart, _anyToSpecialAttackStartCondition);
			AddAnyTransition(_specialAttackEnd, _anyToSpecialAttackEndCondition);
			AddTransition(_specialAttackStart, _specialAttackEnd, _specialAttackStartToEndCondition);
			AddTransition(_specialAttackEnd, _entry, _specialAttackEndToEntryCondition);
			
			AddAnyTransition(_sideSpecialAttackStart, _anyToSideSpecialAttackStartCondition);
			AddAnyTransition(_sideSpecialAttackEnd, _anyToSideSpecialAttackEndCondition);
			AddTransition(_sideSpecialAttackStart, _sideSpecialAttackEnd, _sideSpecialAttackStartToEndCondition);
			AddTransition(_sideSpecialAttackEnd, _entry, _sideSpecialAttackEndToEntryCondition);
			
			AddAnyTransition(_upSpecialAttackStart, _anyToUpSpecialAttackStartCondition);
			AddAnyTransition(_upSpecialAttackEnd, _anyToUpSpecialAttackEndCondition);
			AddTransition(_upSpecialAttackStart, _upSpecialAttackEnd, _upSpecialAttackStartToEndCondition);
			AddTransition(_upSpecialAttackEnd, _entry, _upSpecialAttackEndToEntryCondition);
			
			AddAnyTransition(_downSpecialAttackStart, _anyToDownSpecialAttackStartCondition);
			AddAnyTransition(_downSpecialAttackEnd, _anyToDownSpecialAttackEndCondition);
			AddTransition(_downSpecialAttackStart, _downSpecialAttackEnd, _downSpecialAttackStartToEndCondition);
			AddTransition(_downSpecialAttackEnd, _entry, _downSpecialAttackEndToEntryCondition);
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

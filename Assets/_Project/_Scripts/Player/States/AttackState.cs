namespace Smash.Player.States
{
	// TODO: later check if the attack state logic is same, then rather than using so many separate methods for each attack state, pass in the type of the attack state
	public class MainAttackStart : PlayerBaseState
	{
		public MainAttackStart(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetMainAttackWindup();
		}
	}
	
	public class MainAttackEnd : PlayerBaseState
	{
		public MainAttackEnd(PlayerPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetMainAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetMainAttackFinish();
		}
	}

	public class SideMainAttackStart : PlayerBaseState
	{
		public SideMainAttackStart(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSideMainAttackWindUp();
		}
	}
	
	public class SideMainAttackEnd : PlayerBaseState
	{
		public SideMainAttackEnd(PlayerPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSideMainAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetSideMainAttackFinish();
		}
	}
	
	public class UpMainAttackStart : PlayerBaseState
	{
		public UpMainAttackStart(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetUpMainAttackWindUp();
		}
	}
	
	public class UpMainAttackEnd : PlayerBaseState
	{
		public UpMainAttackEnd(PlayerPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetUpMainAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetUpMainAttackFinish();
		}
	}
	
	public class DownMainAttackStart : PlayerBaseState
	{
		public DownMainAttackStart(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetDownMainAttackWindUp();
		}
	}
	
	public class DownMainAttackEnd : PlayerBaseState
	{
		public DownMainAttackEnd(PlayerPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetDownMainAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetDownMainAttackFinish();
		}
	}
}

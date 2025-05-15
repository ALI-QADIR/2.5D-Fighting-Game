namespace Smash.Player.States
{
	// TODO: later check if the attack state logic is same, then rather than using so many separate methods for each attack state, pass in the type of the attack state
	public class MainAttackStart : PlayerBaseState
	{
		public MainAttackStart(CharacterPawn pawn) : base(pawn)
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
		public MainAttackEnd(CharacterPawn pawn) : base(pawn)
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
		public SideMainAttackStart(CharacterPawn pawn) : base(pawn)
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
		public SideMainAttackEnd(CharacterPawn pawn) : base(pawn)
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
		public UpMainAttackStart(CharacterPawn pawn) : base(pawn)
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
		public UpMainAttackEnd(CharacterPawn pawn) : base(pawn)
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
		public DownMainAttackStart(CharacterPawn pawn) : base(pawn)
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
		public DownMainAttackEnd(CharacterPawn pawn) : base(pawn)
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
	
	public class SpecialAttackStart : PlayerBaseState
	{
		public SpecialAttackStart(CharacterPawn pawn) : base(pawn)
		{
		}	
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSpecialAttackWindup();
		}
	}
	
	public class SpecialAttackEnd : PlayerBaseState
	{
		public SpecialAttackEnd(CharacterPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSpecialAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetSpecialAttackFinish();
		}
	}
	
	public class SideSpecialAttackStart : PlayerBaseState
	{
		public SideSpecialAttackStart(CharacterPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSideSpecialAttackWindUp();
		}
	}
	
	public class SideSpecialAttackEnd : PlayerBaseState
	{
		public SideSpecialAttackEnd(CharacterPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSideSpecialAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetSideSpecialAttackFinish();
		}
	}

	public class UpSpecialAttackStart : PlayerBaseState
	{
		public UpSpecialAttackStart(CharacterPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetUpSpecialAttackWindUp();
		}
	}
	
	public class UpSpecialAttackEnd : PlayerBaseState
	{
		public UpSpecialAttackEnd(CharacterPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetUpSpecialAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetUpSpecialAttackFinish();
		}
	}
	
	public class DownSpecialAttackStart : PlayerBaseState
	{
		public DownSpecialAttackStart(CharacterPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetDownSpecialAttackWindUp();
		}
	}
	
	public class DownSpecialAttackEnd : PlayerBaseState
	{
		public DownSpecialAttackEnd(CharacterPawn pawn) : base(pawn)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetDownSpecialAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetDownSpecialAttackFinish();
		}
	}
}

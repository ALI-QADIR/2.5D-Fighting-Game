namespace Smash.Player.States
{
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
}

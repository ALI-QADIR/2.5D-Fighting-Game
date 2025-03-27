namespace Smash.Player.CommandPattern.ActionCommands
{
	public class ComboSideSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(BasePawn pawn)
		{
		}

		public void FinishActionExecution(BasePawn pawn)
		{
			pawn.HandleSideSpecialAttackInput();
		}
	}
	
	public class ComboUpSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(BasePawn pawn)
		{
		}
		
		public void FinishActionExecution(BasePawn pawn)
		{
			pawn.HandleUpSpecialAttackInput();
		}
	}
	
	public class ComboDownSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(BasePawn pawn)
		{
		}
		
		public void FinishActionExecution(BasePawn pawn)
		{
			pawn.HandleDownSpecialAttackInput();
		}
	}
	
	public class ComboSideMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(BasePawn pawn)
		{
		}
		
		public void FinishActionExecution(BasePawn pawn)
		{
			pawn.HandleSideMainAttackInput();
		}
	}
	
	public class ComboUpMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(BasePawn pawn)
		{
		}
		
		public void FinishActionExecution(BasePawn pawn)
		{
			pawn.HandleUpMainAttackInput();
		}
	}

	public class ComboDownMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(BasePawn pawn)
		{
		}
		
		public void FinishActionExecution(BasePawn pawn)
		{
			pawn.HandleDownMainAttackInput();
		}
	}
}

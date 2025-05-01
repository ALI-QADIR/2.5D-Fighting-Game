namespace Smash.Player.CommandPattern.ActionCommands
{
	public class ComboSideSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
			pawn.HandleSideSpecialAttackInputStart();
		}

		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleSideSpecialAttackInputEnd(HeldDuration);
		}
	}
	
	public class ComboUpSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
			pawn.HandleUpSpecialAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleUpSpecialAttackInputEnd(HeldDuration);
		}
	}
	
	public class ComboDownSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
			pawn.HandleDownSpecialAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleDownSpecialAttackInputEnd(HeldDuration);
		}
	}
	
	public class ComboSideMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
			pawn.HandleSideMainAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleSideMainAttackInputEnd(HeldDuration);
		}
	}
	
	public class ComboUpMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
			pawn.HandleUpMainAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleUpMainAttackInputEnd(HeldDuration);
		}
	}

	public class ComboDownMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
			pawn.HandleDownMainAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleDownMainAttackInputEnd(HeldDuration);
		}
	}
}

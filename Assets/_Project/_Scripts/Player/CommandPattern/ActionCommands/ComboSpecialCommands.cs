namespace Smash.Player.CommandPattern.ActionCommands
{
	public class ComboSideSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}

		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.HandleDashInput();
		}
	}
	
	public class ComboUpSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.HandleLaunchInput();
		}
	}
	
	public class ComboDownSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.HandleLaunchAndFloatInput();
		}
	}
	
	public class ComboSideMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
		}
	}
	
	public class ComboUpMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
		}
	}

	public class ComboDownMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.HandleLaunchAndCrashInput();
		}
	}
}

namespace Smash.Player.CommandPattern.ActionCommands
{
	public class NorthButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "North";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
		}

		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleJumpInput();
		}
	}
	
	public class SouthButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "South";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
			pawn.HandleSpecialAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleSpecialAttackInputEnd(HeldDuration);
		}
	}
	
	public class EastButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "East";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
			pawn.HandleMainAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleMainAttackInputEnd(HeldDuration);
		}
	}
	
	public class WestButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "West";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleJumpInput();
		}
	}
	
	public class DPadUpActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadUp";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleUpInput();
		}
	}
	
	public class DPadDownActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadDown";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleDownInput();
		}
	}
	
	public class DPadLeftActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadLeft";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleLeftInput();
		}
	}
	
	public class DPadRightActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadRight";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleRightInput();
		}
	}
	
	public class DPadNullActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadNull";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler pawn)
		{
		}
		
		public void FinishActionExecution(InputHandler pawn)
		{
			pawn.HandleDpadNullInput();
		}
	}
}

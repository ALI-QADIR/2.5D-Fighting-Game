namespace Smash.Player.CommandPattern.ActionCommands
{
	public class NorthButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "North";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
		}

		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleJumpInput();
		}
	}
	
	public class SouthButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "South";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleSpecialAttackInputStart();
		}
		
		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleSpecialAttackInputEnd(HeldDuration);
		}
	}
	
	public class EastButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "East";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleMainAttackInputStart();
		}
		
		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleMainAttackInputEnd(HeldDuration);
		}
	}
	
	public class WestButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "West";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleJumpInput();
		}
	}
	
	public class DPadUpActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadUp";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleUpInput();
		}
	}
	
	public class DPadDownActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadDown";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleDownInput();
		}
	}
	
	public class DPadLeftActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadLeft";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleLeftInput();
		}
	}
	
	public class DPadRightActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadRight";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleRightInput();
		}
	}
	
	public class DPadNullActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadNull";
		public float HeldDuration { get; set; }
		public int PlayerIndex { get; set; }
		public bool IsFinished { get; set; }

		public void StartActionExecution(IInputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(IInputHandler inputHandler)
		{
			inputHandler.HandleDpadNullInput();
		}
	}
}

namespace Smash.Player.CommandPattern.ActionCommands
{
	public class ComboSideSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Special";
		public float HeldDuration { get; set; }
		private readonly int direction;
		
		public ComboSideSpecialCommand(int direction)
		{
			this.direction = direction;
		}
		
		public void StartActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleSideSpecialAttackInputStart(direction);
		}

		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleSideSpecialAttackInputEnd(HeldDuration, direction);
		}
	}
	
	public class ComboUpSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleUpSpecialAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleUpSpecialAttackInputEnd(HeldDuration);
		}
	}
	
	public class ComboDownSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Special";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleDownSpecialAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleDownSpecialAttackInputEnd(HeldDuration);
		}
	}
	
	public class ComboSideMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Main";
		public float HeldDuration { get; set; }
		private readonly int direction;
		
		public ComboSideMainCommand(int direction)
		{
			this.direction = direction;
		}
		
		public void StartActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleSideMainAttackInputStart(direction);
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleSideMainAttackInputEnd(HeldDuration, direction);
		}
	}
	
	public class ComboUpMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleUpMainAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleUpMainAttackInputEnd(HeldDuration);
		}
	}

	public class ComboDownMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Main";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleDownMainAttackInputStart();
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleDownMainAttackInputEnd(HeldDuration);
		}
	}
}

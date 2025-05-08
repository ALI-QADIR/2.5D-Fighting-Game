namespace Smash.Player.CommandPattern.ActionCommands
{
	public class CancelCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Cancel";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleCancelButton();
		}
	}
	
	public class SubmitCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Submit";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleSubmitButton();
		}
	}
	
	public class HorizontalScrollCommand : IGameplayActionCommand
	{
		public HorizontalScrollCommand(float direction)
		{
			m_direction = direction > 0 ? 1 : -1;
		}
		public string ActionName { get; } = "HorizontalScroll";
		public float HeldDuration { get; set; }

		private readonly int m_direction;

		public void StartActionExecution(InputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleHorizontalScrollInput(m_direction);
		}
	}
	
	public class VerticalScrollCommand : IGameplayActionCommand
	{
		public VerticalScrollCommand(float direction)
		{
			m_direction = direction > 0 ? 1 : -1;
		}
		public string ActionName { get; } = "VerticalScroll";
		public float HeldDuration { get; set; }

		private readonly int m_direction;

		public void StartActionExecution(InputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleVerticalScrollInput(m_direction);
		}
	}
	
	public class ShoulderButtonCommand : IGameplayActionCommand
	{
		public ShoulderButtonCommand(float direction)
		{
			m_direction = direction > 0 ? 1 : -1;
		}
		public string ActionName { get; } = "ShoulderButton";
		public float HeldDuration { get; set; }

		private readonly int m_direction;

		public void StartActionExecution(InputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleShoulderButtonInput(m_direction);
		}
	}
	
	public class ShoulderTriggerCommand : IGameplayActionCommand
	{
		public ShoulderTriggerCommand(float direction)
		{
			m_direction = direction > 0 ? 1 : -1;
		}
		public string ActionName { get; } = "ShoulderTrigger";
		public float HeldDuration { get; set; }

		private readonly int m_direction;

		public void StartActionExecution(InputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleShoulderTriggerInput(m_direction);
		}
	}
	
	public class ResumeCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Resume";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(InputHandler inputHandler)
		{
		}
		
		public void FinishActionExecution(InputHandler inputHandler)
		{
			inputHandler.HandleResumeInput();
		}
	}
}

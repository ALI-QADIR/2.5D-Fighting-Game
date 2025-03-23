namespace Smash.Player.CommandPattern.ActionCommands
{
	public class ComboSideSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Special";
		public float HeldDuration { get; set; }

		public void ExecuteAction()
		{
		}
	}
	
	public class ComboUpSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Special";
		public float HeldDuration { get; set; }
		
		public void ExecuteAction()
		{
			
		}
	}
	
	public class ComboDownSpecialCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Special";
		public float HeldDuration { get; set; }
		public void ExecuteAction()
		{
			
		}
	}
	
	public class ComboSideMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Side Main";
		public float HeldDuration { get; set; }
		public void ExecuteAction()
		{
			
		}
	}
	
	public class ComboUpMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Up Main";
		public float HeldDuration { get; set; }
		public void ExecuteAction()
		{
			
		}
	}

	public class ComboDownMainCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "Combo Down Main";
		public float HeldDuration { get; set; }
		public void ExecuteAction()
		{
		}
	}
}

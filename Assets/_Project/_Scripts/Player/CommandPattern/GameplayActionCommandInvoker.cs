namespace Smash.Player.CommandPattern
{
	public class GameplayActionCommandInvoker
	{
		public void ExecuteCommand(IGameplayActionCommand command)
		{
			command.ExecuteAction();
		}
	}
}

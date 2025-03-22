namespace Smash.Player.CommandPattern.ActionCommands
{
	public interface IGameplayActionCommand
	{
		public string ActionName { get; }
		void ExecuteAction();
	}
}

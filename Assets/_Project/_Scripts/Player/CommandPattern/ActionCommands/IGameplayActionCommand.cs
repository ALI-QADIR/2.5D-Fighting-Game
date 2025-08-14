namespace Smash.Player.CommandPattern.ActionCommands
{
	public interface IGameplayActionCommand
	{
		string ActionName { get; }
		float HeldDuration { get; set; }
		int PlayerIndex { get; set; }
		bool IsFinished { get; set; }
		void StartActionExecution(IInputHandler inputHandler);
		void FinishActionExecution(IInputHandler inputHandler);

		void SetCommandParameters(int playerIndex, bool isFinished = false)
		{
			PlayerIndex = playerIndex;
			IsFinished = isFinished;
		}
	}
}

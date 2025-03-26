namespace Smash.Player.CommandPattern.ActionCommands
{
	public interface IGameplayActionCommand
	{
		string ActionName { get; }
		float HeldDuration { get; set; }
		void StartActionExecution(PlayerPawn pawn);
		void FinishActionExecution(PlayerPawn pawn);
	}
}

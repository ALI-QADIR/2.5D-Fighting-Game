using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player
{
	public class PlayerUiController : PlayerController
	{
		#region Public Methods

		public override void Initialise(UiPawn pawn)
		{
			base.Initialise(pawn);
			Initialise();
		}

		#endregion Public Methods

		#region Private Methods

		protected override void Initialise()
		{
			base.Initialise();
		}
		
		protected override void OnCommandExecutionStarted(IGameplayActionCommand command)
		{
			// no-op
		}

		protected override void OnCommandExecutionFinished(IGameplayActionCommand command)
		{
			// no-op
		}

		#endregion Private Methods
	}
}

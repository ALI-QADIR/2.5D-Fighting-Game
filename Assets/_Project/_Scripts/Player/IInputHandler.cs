using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player
{
	public interface IInputHandler
	{
		int PlayerIndex { get; set; }
		public void HandleRightInput();

		public void HandleLeftInput();

		public void HandleDpadNullInput();

		public void HandleUpInput();

		public void HandleDownInput();

		public void HandleJumpInput();

		public void HandleMainAttackInputStart();

		public void HandleMainAttackInputEnd(float heldTime);

		public void HandleSideMainAttackInputStart(int direction);

		public void HandleSideMainAttackInputEnd(float heldTime, int direction);

		public void HandleUpMainAttackInputStart();

		public void HandleUpMainAttackInputEnd(float heldTime);

		public void HandleDownMainAttackInputStart();

		public void HandleDownMainAttackInputEnd(float heldTime);

		public void HandleSpecialAttackInputStart();

		public void HandleSpecialAttackInputEnd(float heldTime);

		public void HandleSideSpecialAttackInputStart(int direction);

		public void HandleSideSpecialAttackInputEnd(float heldTime, int direction);

		public void HandleDownSpecialAttackInputStart();

		public void HandleDownSpecialAttackInputEnd(float heldTime);

		public void HandleUpSpecialAttackInputStart();
		public void HandleUpSpecialAttackInputEnd(float heldTime);
		public void HandleCancelButton();

		public void HandleCancelButton(IGameplayActionCommand command);

		public void HandleSubmitButton(IGameplayActionCommand command);

		public void HandleHorizontalScrollInput(IGameplayActionCommand command);

		public void HandleVerticalScrollInput(IGameplayActionCommand command);

		public void HandleShoulderTriggerInput(IGameplayActionCommand command);

		public void HandleShoulderButtonInput(IGameplayActionCommand command);

		public void HandleResumeInput(IGameplayActionCommand command);
	}
}

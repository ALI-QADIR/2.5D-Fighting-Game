using UnityEngine.InputSystem;

namespace Smash.Player.CommandPattern
{
	public class ButtonActionController : ActionController
	{
		protected override void SetupActions()
		{
			InputActions.Player.North.performed += HandleNorthInput;
			InputActions.Player.South.performed += HandleSouthInput;
			InputActions.Player.East.performed += HandleEastInput;
			InputActions.Player.West.performed += HandleWestInput;
		}

		protected override void RemoveActions()
		{
			InputActions.Player.North.performed -= HandleNorthInput;
			InputActions.Player.South.performed -= HandleSouthInput;
			InputActions.Player.East.performed -= HandleEastInput;
			InputActions.Player.West.performed -= HandleWestInput;
		}

		private void HandleNorthInput(InputAction.CallbackContext ctx)
		{
			var northButtonCommand = new NorthButtonActionCommand();
			AddToSequence(northButtonCommand);
			ExecuteActionCommand(northButtonCommand);
		}
		
		private void HandleSouthInput(InputAction.CallbackContext ctx)
		{
			
		}
		
		private void HandleEastInput(InputAction.CallbackContext ctx)
		{
			
		}
		
		private void HandleWestInput(InputAction.CallbackContext ctx)
		{
			
		}

		protected override void AddToSequence(IGameplayActionCommand command)
		{
			ComboActionQueueManager.AddCommandToComboSequence(command);
		}
		
		protected override void ExecuteActionCommand(IGameplayActionCommand command)
		{
			Invoker.ExecuteCommand(command);
		}
	}
}

using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine.InputSystem;

namespace Smash.Player.CommandPattern.Controllers
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
			var southButtonCommand = new SouthButtonActionCommand();
			AddToSequence(southButtonCommand);
			ExecuteActionCommand(southButtonCommand);
		}
		
		private void HandleEastInput(InputAction.CallbackContext ctx)
		{
			var eastButtonCommand = new EastButtonActionCommand();
			AddToSequence(eastButtonCommand);
			ExecuteActionCommand(eastButtonCommand);
		}
		
		private void HandleWestInput(InputAction.CallbackContext ctx)
		{
			var westButtonCommand = new WestButtonActionCommand();
			AddToSequence(westButtonCommand);
			ExecuteActionCommand(westButtonCommand);
		}
	}
}

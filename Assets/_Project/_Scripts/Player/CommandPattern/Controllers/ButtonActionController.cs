using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine.InputSystem;

namespace Smash.Player.CommandPattern.Controllers
{
	public class ButtonActionController : ActionController
	{
		protected override void SetupActions()
		{
			InputActions.Player.North.performed += HandleNorthInputPerformed;
			
			InputActions.Player.South.performed += HandleSouthInputPerformed;
			InputActions.Player.South.canceled += HandleSouthInputCanceled;
			
			InputActions.Player.East.performed += HandleEastInputPerformed;
			InputActions.Player.East.canceled += HandleEastInputCanceled;
			
			InputActions.Player.West.performed += HandleWestInputPerformed;
		}

		protected override void RemoveActions()
		{
			InputActions.Player.North.performed -= HandleNorthInputPerformed;
			
			InputActions.Player.South.performed -= HandleSouthInputPerformed;
			InputActions.Player.South.canceled -= HandleSouthInputCanceled;
			
			InputActions.Player.East.performed -= HandleEastInputPerformed;
			InputActions.Player.East.canceled -= HandleEastInputCanceled;
			
			InputActions.Player.West.performed -= HandleWestInputPerformed;
		}

		private void HandleNorthInputPerformed(InputAction.CallbackContext _)
		{
			var northButtonCommand = new NorthButtonActionCommand();
			AddToSequence(northButtonCommand);
		}
		
		private void HandleSouthInputPerformed(InputAction.CallbackContext _)
		{
			var southButtonCommand = new SouthButtonActionCommand();
			AddToSequence(southButtonCommand);
		}
		
		private void HandleSouthInputCanceled(InputAction.CallbackContext _)
		{
			_comboActionQueueManager.TryExecuteCachedCombo();
		}
		
		private void HandleEastInputPerformed(InputAction.CallbackContext _)
		{
			var eastButtonCommand = new EastButtonActionCommand();
			AddToSequence(eastButtonCommand);
		}
		
		private void HandleEastInputCanceled(InputAction.CallbackContext _)
		{
			_comboActionQueueManager.TryExecuteCachedCombo();
		}
		
		private void HandleWestInputPerformed(InputAction.CallbackContext _)
		{
			var westButtonCommand = new WestButtonActionCommand();
			AddToSequence(westButtonCommand);
		}
	}
}

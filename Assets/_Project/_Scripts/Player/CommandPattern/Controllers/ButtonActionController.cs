using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine.InputSystem;

namespace Smash.Player.CommandPattern.Controllers
{
	public class ButtonActionController : ActionController
	{
		protected override void SetupActions()
		{
			_inputActionsController.NorthPerformed += HandleNorthInputPerformed;
			
			_inputActionsController.SouthPerformed += HandleSouthInputPerformed;
			_inputActionsController.SouthCanceled += HandleSouthInputCanceled;

			_inputActionsController.EastPerformed += HandleEastInputPerformed;
			_inputActionsController.EastCanceled += HandleEastInputCanceled;
			
			_inputActionsController.WestPerformed += HandleWestInputPerformed;
		}

		protected override void RemoveActions()
		{
			_inputActionsController.NorthPerformed -= HandleNorthInputPerformed;
			
			_inputActionsController.SouthPerformed -= HandleSouthInputPerformed;
			_inputActionsController.SouthCanceled -= HandleSouthInputCanceled;

			_inputActionsController.EastPerformed -= HandleEastInputPerformed;
			_inputActionsController.EastCanceled -= HandleEastInputCanceled;
			
			_inputActionsController.WestPerformed -= HandleWestInputPerformed;
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

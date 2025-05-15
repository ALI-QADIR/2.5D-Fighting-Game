using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine.InputSystem;

namespace Smash.Player.CommandPattern.Controllers
{
	public class UiActionController : ActionController
	{
		protected override void SetupActions()
		{
			InputActions.UI.Cancel.performed += HandleCancelInputPerformed;
			InputActions.UI.Submit.performed += HandleSubmitInputPerformed;
			InputActions.UI.HorizontalScroll.performed += HandleHorizontalScrollInputPerformed;
			InputActions.UI.VerticalScroll.performed += HandleVerticalScrollInputPerformed;
			InputActions.UI.ShoulderButton.performed += HandleShoulderButtonInputPerformed;
			InputActions.UI.ShoulderTrigger.performed += HandleShoulderTriggerInputPerformed;
			InputActions.UI.Resume.performed += HandleResumeInputPerformed;
		}

		protected override void RemoveActions()
		{
			InputActions.UI.Cancel.performed -= HandleCancelInputPerformed;
			InputActions.UI.Submit.performed -= HandleSubmitInputPerformed;
			InputActions.UI.HorizontalScroll.performed -= HandleHorizontalScrollInputPerformed;
			InputActions.UI.VerticalScroll.performed -= HandleVerticalScrollInputPerformed;
			InputActions.UI.ShoulderButton.performed -= HandleShoulderButtonInputPerformed;
			InputActions.UI.ShoulderTrigger.performed -= HandleShoulderTriggerInputPerformed;
			InputActions.UI.Resume.performed -= HandleResumeInputPerformed;
		}

		private void HandleCancelInputPerformed(InputAction.CallbackContext ctx)
		{
			var cancelCommand = new CancelCommand();
			AddToSequence(cancelCommand);
		}
		
		private void HandleSubmitInputPerformed(InputAction.CallbackContext obj)
		{
			var submitCommand = new SubmitCommand();
			AddToSequence(submitCommand);
		}

		private void HandleHorizontalScrollInputPerformed(InputAction.CallbackContext obj)
		{
			var horizontalScrollCommand = new HorizontalScrollCommand(obj.ReadValue<float>());
			AddToSequence(horizontalScrollCommand);
		}

		private void HandleVerticalScrollInputPerformed(InputAction.CallbackContext obj)
		{
			var verticalScrollCommand = new VerticalScrollCommand(obj.ReadValue<float>());
			AddToSequence(verticalScrollCommand);
		}

		private void HandleShoulderButtonInputPerformed(InputAction.CallbackContext obj)
		{
			var shoulderButtonCommand = new ShoulderButtonCommand(obj.ReadValue<float>());
			AddToSequence(shoulderButtonCommand);
		}

		private void HandleShoulderTriggerInputPerformed(InputAction.CallbackContext obj)
		{
			var shoulderTriggerCommand = new ShoulderTriggerCommand(obj.ReadValue<float>());
			AddToSequence(shoulderTriggerCommand);
		}

		private void HandleResumeInputPerformed(InputAction.CallbackContext obj)
		{
			var resumeCommand = new ResumeCommand();
			AddToSequence(resumeCommand);
		}
	}
}

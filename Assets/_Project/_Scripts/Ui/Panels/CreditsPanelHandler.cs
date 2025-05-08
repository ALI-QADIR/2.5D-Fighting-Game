using System;
using Smash.System;
using Smash.Ui.System;

namespace Smash.Ui.Panels
{
	public class CreditsPanelHandler : PanelHandler
	{
		protected override void Awake()
		{
			base.Awake();
			_eventDictionary.Add("btn_credits_back", OnClickBackButton);
			_eventDictionary.Add("btn_main_credits", OnClickCreditsButton);
			_backButtonHandler.SetEventArgs("btn_credits_back", this);
		}

		protected override void AuthenticateEvent(UiEventArgs args)
		{
			if (_eventDictionary.TryGetValue(args.id.ToLower(), out Action action))
				action?.Invoke();
		}

		public override void OpenPanel()
		{
			base.OpenPanel();
			/*_input.UI.Cancel.Enable();
			_input.UI.Cancel.started += BackButtonPressed;*/
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			// _input.UI.Cancel.started -= BackButtonPressed;
		}

		protected override void BackButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			_backButtonHandler.BackButtonPressed();
		}
		
		private void OnClickBackButton()
		{
			ClosePanel();
		}

		private void OnClickCreditsButton()
		{
			OpenPanel();
		}
	}
}

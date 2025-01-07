using System;
using Smash.System;
using Smash.Ui.System;
using UnityEngine.InputSystem;

namespace Smash.Ui.Panels
{
	public class ModeSelectPanelHandler : PanelHandler
	{
		protected override void Awake()
		{
			base.Awake();
			_eventDictionary.Add("btn_main_play", OnClickPlayButton);
			_eventDictionary.Add("btn_modes_back", OnClickBackButton);
			_backButtonHandler.SetEventArgs("btn_modes_back", this);
		}

		protected override void AuthenticateEvent(UiEventArgs args)
		{
			if (_eventDictionary.TryGetValue(args.id.ToLower(), out Action action))
				action?.Invoke();
		}

		public override void OpenPanel()
		{
			base.OpenPanel();
			_input.UI.Cancel.Enable();
			_input.UI.Cancel.started += BackButtonPressed;
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			_input.UI.Cancel.started -= BackButtonPressed;
		}

		protected override void BackButtonPressed(InputAction.CallbackContext ctx)
		{
			_backButtonHandler.BackButtonPressed();
		}

		private void OnClickPlayButton()
		{
			OpenPanel();
		}
		
		private void OnClickBackButton()
		{
			ClosePanel();
		}
	}
}
using System;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.StructsAndEnums;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.Panels
{
	public class OptionsPanelHandler : PanelHandler
	{
		[Header("Options")] 
		[SerializeField] private PlayerSettingsController m_settingsController;
		
		protected override void Awake()
		{
			base.Awake();
			// _eventDictionary.Add("btn_options_back", OnClickBackButton);
			// _eventDictionary.Add("btn_options_sfx",m_settingsController.OnClickSfxButton);
			// _eventDictionary.Add("btn_options_sfx_vol", m_settingsController.OnClickSfxVolButton);
			// _eventDictionary.Add("btn_options_touch_controls", m_settingsController.OnClickTouchControlsButton);
			// _eventDictionary.Add("btn_main_options", OnClickOptionsButton);
			
			// _backButtonHandler.SetEventArgs("btn_options_back", this);
		}

		protected override void AuthenticateEvent(IGameplayActionCommand uiCommand)
		{
			if (_panelState != PanelState.Open) return;
			if (_eventDictionary.TryGetValue(uiCommand.GetType(), out Action action))
				action?.Invoke();
		}

		public override void OpenPanel()
		{
			base.OpenPanel();
			// _input.UI.Cancel.Enable();
			// _input.UI.Cancel.started += BackButtonPressed;
			
			m_settingsController.EnableSettingsControl();
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			// _input.UI.Cancel.started -= BackButtonPressed;
			
			m_settingsController.DisableSettingsControl();
		}

		protected override void BackButtonPressed()
		{
			_backButtonHandler.BackButtonPressed();
		}
		
		private void OnClickBackButton()
		{
			ClosePanel();
		}

		private void OnClickOptionsButton()
		{
			OpenPanel();
		}
	}
}

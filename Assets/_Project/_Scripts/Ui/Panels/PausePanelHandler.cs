﻿using System;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.Panels
{
	public class PausePanelHandler : PanelHandler
	{
		[Header("Options")] [SerializeField] private PlayerSettingsController m_settingsController;
		
		protected override void Awake()
		{
			base.Awake();
			_eventDictionary.Add("btn_player_pause", OnClickPauseButton);
			
			_eventDictionary.Add("btn_pause_resume", OnClickResumeButton);
			_eventDictionary.Add("btn_pause_sfx",m_settingsController.OnClickSfxButton);
			_eventDictionary.Add("btn_pause_sfx_vol", m_settingsController.OnClickSfxVolButton);
			_eventDictionary.Add("btn_pause_touch_controls", m_settingsController.OnClickTouchControlsButton);
			_eventDictionary.Add("btn_pause_mainmenu", OnClickMainMenuButton);
			
			_backButtonHandler.SetEventArgs("btn_pause_resume", this);
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
			_input.UI.Resume.Enable();
			_input.UI.Cancel.started += BackButtonPressed;
			_input.UI.Resume.started += BackButtonPressed;
			
			m_settingsController.EnableSettingsControl();
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			_input.UI.Cancel.started -= BackButtonPressed;
			_input.UI.Resume.started -= BackButtonPressed;
			
			m_settingsController.DisableSettingsControl();
		}

		protected override void BackButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			_backButtonHandler.BackButtonPressed();
		}
		
		private void OnClickResumeButton()
		{
			ClosePanel();
		}

		private void OnClickPauseButton()
		{
			OpenPanel();
		}

		private void OnClickMainMenuButton()
		{
			if(!AsyncSceneLoader.HasInstance) return;
			_input.UI.Disable();
			AsyncSceneLoader.Instance.LoadSceneGroupByIndex(0);
		}
	}
}
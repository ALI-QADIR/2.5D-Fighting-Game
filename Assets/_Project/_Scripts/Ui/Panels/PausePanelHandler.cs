using System;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.StructsAndEnums;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.Panels
{
	public class PausePanelHandler : PanelHandler
	{
		[Header("Options")] [SerializeField] private PlayerSettingsController m_settingsController;
		[Header("Pause")] [SerializeField] private GameObject m_retryButton;
		
		protected override void Awake()
		{
			base.Awake();
			// _eventDictionary.Add("btn_player_pause", OnClickPauseButton);
			// 
			// _eventDictionary.Add("btn_pause_resume", OnClickResumeButton);
			// _eventDictionary.Add("btn_pause_sfx",m_settingsController.OnClickSfxButton);
			// _eventDictionary.Add("btn_pause_sfx_vol", m_settingsController.OnClickSfxVolButton);
			// _eventDictionary.Add("btn_pause_touch_controls", m_settingsController.OnClickTouchControlsButton);
			// _eventDictionary.Add("btn_pause_mainmenu", OnClickMainMenuButton);
			// _eventDictionary.Add("btn_pause_retry", OnClickRetry);
			
			// _backButtonHandler.SetEventArgs("btn_pause_resume", this);
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
			
			m_retryButton.SetActive(false);
			if (SpeedRunManager.HasInstance) ActivateSpeedRunOptionsPanel();
			
			/*_input.UI.Cancel.Enable();
			_input.UI.Resume.Enable();
			_input.UI.Cancel.started += BackButtonPressed;
			_input.UI.Resume.started += BackButtonPressed;*/
			
			m_settingsController.EnableSettingsControl();
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			/*_input.UI.Cancel.started -= BackButtonPressed;
			_input.UI.Resume.started -= BackButtonPressed;*/
			
			m_settingsController.DisableSettingsControl();
		}

		protected override void BackButtonPressed()
		{
			_backButtonHandler.BackButtonPressed();
			if (AudioManager.HasInstance)
				AudioManager.Instance.PlayButtonClick();
		}
		
		private void OnClickResumeButton()
		{
			if (SpeedRunManager.HasInstance)
				SpeedRunManager.Instance.PauseTimer(false);
			ClosePanel();
		}

		private void OnClickRetry()
		{
			if(!SpeedRunManager.HasInstance) return;
			SpeedRunManager.Instance.ShouldBeginCountdown();
			ClosePanel();
		}

		private void ActivateSpeedRunOptionsPanel()
		{
			m_retryButton.SetActive(true);
			
			SpeedRunManager.Instance.PauseTimer(true);
		}

		private void OnClickPauseButton()
		{
			OpenPanel();
			if (AudioManager.HasInstance)
				AudioManager.Instance.PlayButtonClick();
		}

		private void OnClickMainMenuButton()
		{
			if(!AsyncSceneLoader.HasInstance) return;
			// _input.UI.Disable();
			AsyncSceneLoader.Instance.LoadSceneByIndex(0);
		}
	}
}

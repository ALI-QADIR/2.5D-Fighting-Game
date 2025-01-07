using System;
using Smash.System;
using TMPro;
using UnityEngine;

namespace Smash.Ui
{
	public class OptionsPanelHandler : PanelHandler
	{
		[Space(15)]
		[SerializeField] private PlayerSettings m_playerSettings;
		
		[Space(15)]
		[SerializeField] private TMP_Text m_sfxStateText;
		[SerializeField] private TMP_Text m_sfxVolText;
		[SerializeField] private TMP_Text m_touchControlsText;
		[SerializeField] private TMP_ColorGradient m_onGradient;
		[SerializeField] private TMP_ColorGradient m_offGradient;
		
		protected override void Awake()
		{
			base.Awake();
			_eventDictionary.Add("btn_options_back", OnClickBackButton);
			_eventDictionary.Add("btn_options_sfx", OnClickSfxButton);
			_eventDictionary.Add("btn_options_sfx_vol", OnClickSfxVolButton);
			_eventDictionary.Add("btn_options_touch_controls", OnClickTouchControlsButton);
			_eventDictionary.Add("btn_main_options", OnClickCreditsButton);
			
			_backButtonHandler.SetEventArgs("btn_options_back", this);
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
			
			m_playerSettings.AddSfxListener(SfxToggled);
			m_playerSettings.AddTouchControlsListener(TouchControlsToggled);
			
			TouchControlsToggled(m_playerSettings.GetTouchControls);
			SfxToggled(m_playerSettings.GetSfx);
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			_input.UI.Cancel.started -= BackButtonPressed;
			
			m_playerSettings.RemoveSfxListener(SfxToggled);
			m_playerSettings.RemoveTouchControlsListener(TouchControlsToggled);
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
		
		private void OnClickSfxButton()
		{
			//TODO: Implement on/off using arrow keys : On select -> enable arrow keys -> On deselect -> disable arrow keys
			m_playerSettings.SetSfx(!m_playerSettings.GetSfx);
		}
		
		private void OnClickSfxVolButton()
		{
			//TODO: Implement on/off using arrow keys : On select -> enable arrow keys -> On deselect -> disable arrow keys
			throw new NotImplementedException();
		}
		
		private void OnClickTouchControlsButton()
		{
			//TODO: Implement on/off using arrow keys : On select -> enable arrow keys -> On deselect -> disable arrow keys
			m_playerSettings.SetTouchControls(!m_playerSettings.GetTouchControls);
		}

		private void SfxToggled(bool value)
		{
			if (value)
			{
				m_sfxStateText.SetText("on");
				m_sfxStateText.colorGradientPreset = m_onGradient;
			}
			else
			{
				m_sfxStateText.SetText("off");
				m_sfxStateText.colorGradientPreset = m_offGradient;
			}
		}
		
		private void TouchControlsToggled(bool value)
		{
			if (value)
			{
				m_touchControlsText.SetText("on");
				m_touchControlsText.colorGradientPreset = m_onGradient;
			}
			else
			{
				m_touchControlsText.SetText("off");
				m_touchControlsText.colorGradientPreset = m_offGradient;
			}
		}
	}
}
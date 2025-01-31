using System;
using Smash.System;
using TMPro;
using UnityEngine;

namespace Smash.Ui
{
	public class PlayerSettingsController : MonoBehaviour
	{
		[SerializeField] private PlayerSettingsSO m_playerSettings;
		
		[Space(15)]
		[SerializeField] private TMP_Text m_sfxStateText;
		[SerializeField] private TMP_Text m_sfxVolText;
		[SerializeField] private TMP_Text m_touchControlsText;
		[SerializeField] private TMP_ColorGradient m_onGradient;
		[SerializeField] private TMP_ColorGradient m_offGradient;

		public void EnableSettingsControl()
		{
			m_playerSettings.AddSfxListener(SfxToggled);
			m_playerSettings.AddTouchControlsListener(TouchControlsToggled);
			
			TouchControlsToggled(m_playerSettings.GetTouchControls);
			SfxToggled(m_playerSettings.GetSfx);
		}

		public void DisableSettingsControl()
		{
			m_playerSettings.RemoveSfxListener(SfxToggled);
			m_playerSettings.RemoveTouchControlsListener(TouchControlsToggled);
		}
		
		public void OnClickSfxButton()
		{
			//TODO: Implement on/off using arrow keys : On select -> enable arrow keys -> On deselect -> disable arrow keys
			m_playerSettings.SetSfx(!m_playerSettings.GetSfx);
		}
		
		public void OnClickSfxVolButton()
		{
			//TODO: Implement on/off using arrow keys : On select -> enable arrow keys -> On deselect -> disable arrow keys
			throw new NotImplementedException();
		}
		
		public void OnClickTouchControlsButton()
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

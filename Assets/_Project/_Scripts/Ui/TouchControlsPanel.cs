using Smash.System;
using UnityEngine;

namespace Smash.Ui
{
	public class TouchControlsPanel : MonoBehaviour
	{
		[SerializeField] private PlayerSettingsSO m_settings;
		[SerializeField] private GameObject m_panel;

		private void OnEnable()
		{
			EnablePanel(m_settings.GetTouchControls);
			m_settings.AddTouchControlsListener(EnablePanel);
		}

		private void OnDisable()
		{
			m_settings.RemoveTouchControlsListener(EnablePanel);
		}

		private void EnablePanel(bool enable) => m_panel.SetActive(enable);
	}
}
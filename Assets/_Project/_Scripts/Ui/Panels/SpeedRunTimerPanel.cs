using System;
using TMPro;
using UnityEngine;

namespace Smash.Ui.Panels
{
	public class SpeedRunTimerPanel : MonoBehaviour
	{
		[SerializeField] private TMP_Text m_timerText;
		
		private float m_time;
		public float Time
		{
			get => m_time;
			set
			{
				m_time = value;
				m_timerText.text = m_time.ToString("f3");
			}
		}

		private void Awake()
		{
			m_timerText.text = String.Empty;
		}
		
		public void SetText(string message) => m_timerText.text = message;
	}
}
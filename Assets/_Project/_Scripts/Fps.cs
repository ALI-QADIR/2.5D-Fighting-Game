using System;
using TMPro;
using UnityEngine;

namespace Smash
{
	public class Fps : MonoBehaviour
	{
		public TMP_Text fpsText;
		public float refreshTime = 0.5f;
		public bool showFps;

		//Declare these in your class
		int m_frameCounter = 0;
		float m_timeCounter = 0.0f;
		float m_lastFramerate = 0.0f;

		private void Awake()
		{
			gameObject.SetActive(showFps);
		}

		private void Update()
		{
			if( m_timeCounter < refreshTime )
			{
				m_timeCounter += Time.deltaTime;
				m_frameCounter++;
			}
			else
			{
				//This code will break if you set your m_refreshTime to 0, which makes no sense.
				m_lastFramerate = m_frameCounter/m_timeCounter;
				m_frameCounter = 0;
				m_timeCounter = 0.0f;
				fpsText.text = "FPS: " + m_lastFramerate.ToString("f2");
			}
		}
	}
}

using UnityEngine;
using UnityEngine.UI;

namespace Smash.Ui.Components
{
	public class ScrollHandler : MonoBehaviour
	{
		[SerializeField] private float m_scrollSpeed;
		[SerializeField] private Scrollbar m_scrollbar;
		public float axisValue;

		private float m_currentScrollBarValue;
		
		private void Update()
		{
			if (axisValue == 0f) return;
			m_currentScrollBarValue = m_scrollbar.value;
			m_currentScrollBarValue += m_scrollSpeed * axisValue * Time.deltaTime;
			m_currentScrollBarValue = Mathf.Clamp01(m_currentScrollBarValue);
			
			m_scrollbar.value = m_currentScrollBarValue;
		}
	}
}
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.Components
{
	public class BackButtonHandler : MonoBehaviour
	{
		[SerializeField] private PanelHandler m_previousPanel;
		public void HandleBackButton()
		{
			m_previousPanel.OpenPanel();
		}
	}
}
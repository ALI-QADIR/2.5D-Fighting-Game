using System.Collections;
using Smash.System;
using TripleA.EventSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Smash.Ui
{
	public abstract class PanelHandler : EventListener<UiEventArgs>
	{
		[SerializeField] private ButtonSelectionHandler m_primaryButton;

		protected virtual IEnumerator Start()
		{
			yield return null;
			EventSystem.current.SetSelectedGameObject(m_primaryButton.gameObject);
		}
	}
}
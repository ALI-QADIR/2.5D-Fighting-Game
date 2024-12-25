using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Smash.Ui
{
	public abstract class PanelHandler : MonoBehaviour
	{
		[SerializeField] private Selectable m_primaryButton;
		private GameObject m_lastSelected;

		protected virtual IEnumerator Start()
		{
			yield return null;
			EventSystem.current.SetSelectedGameObject(m_primaryButton.gameObject);
		}
		
		private void OnEnable()
		{
			ButtonSelectionHandler.OnButtonDeselected += OnButtonDeselected;
		}


		private void OnDisable()
		{
			ButtonSelectionHandler.OnButtonDeselected -= OnButtonDeselected;
		}

		private void OnButtonDeselected(GameObject obj)
		{
			m_lastSelected = obj;
		}
		
		private void OnNavigateStart(InputAction.CallbackContext ctx)
		{
			if (EventSystem.current.currentSelectedGameObject != null) return;
			if (m_lastSelected == null) EventSystem.current.SetSelectedGameObject(m_primaryButton.gameObject);
			else EventSystem.current.SetSelectedGameObject(m_lastSelected);
		}
	}
}
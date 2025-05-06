using System;
using System.Collections;
using Smash.Ui.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Smash.Ui.System
{
	public abstract class PanelHandler : UiEventListener
	{
		[Header("Animation")] 
		[SerializeField] protected AnimationStrategy<Action> _animationStrategy;

		[Space(10)]
		[SerializeField] private ButtonSelectionHandler m_primaryButton;
		[SerializeField] protected BackButtonHandler _backButtonHandler;
		private GameObject m_lastSelected;

		protected PlayerInputActions _input;
		
		protected override void Awake()
		{
			base.Awake();
			_input = new PlayerInputActions();
			gameObject.SetActive(false);
		}

		protected virtual IEnumerator SetSelected()
		{
			yield return null;
			EventSystem.current.SetSelectedGameObject(m_primaryButton.gameObject);
		}

		private void OnButtonDeselected(GameObject obj)
		{
			m_lastSelected = obj;
		}
		
		protected void OnNavigateStart(InputAction.CallbackContext ctx)
		{
			if (EventSystem.current.currentSelectedGameObject != null) return;
			if (m_lastSelected == null) EventSystem.current.SetSelectedGameObject(m_primaryButton.gameObject);
			else EventSystem.current.SetSelectedGameObject(m_lastSelected);
		}

		public virtual void OpenPanel()
		{
			_animationStrategy.Activate(OnComplete);
			
			ButtonSelectionHandler.OnButtonDeselected += OnButtonDeselected;
			
			_input.Player.Disable();
			_input.UI.Disable();
			_input.UI.Enable();
			
			_input.UI.Cancel.Disable();
			_input.UI.Retry.Disable();
			_input.UI.Resume.Disable();
			_input.UI.HorizontalScroll.Disable();
			_input.UI.VerticalScroll.Disable();
			_input.UI.Navigate.Enable();
			_input.UI.Navigate.performed += OnNavigateStart;
		}
		
		public virtual void ClosePanel()
		{
			_animationStrategy.Deactivate();
			
			ButtonSelectionHandler.OnButtonDeselected -= OnButtonDeselected;
			
			_input.UI.Navigate.performed -= OnNavigateStart;
		}
		
		private void OnComplete() => StartCoroutine(SetSelected());

		protected abstract void BackButtonPressed(InputAction.CallbackContext ctx);

		protected override void OnDestroy()
		{
			ButtonSelectionHandler.OnButtonDeselected -= OnButtonDeselected;
		}
	}
}

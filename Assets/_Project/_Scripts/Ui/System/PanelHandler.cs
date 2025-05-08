using System;
using System.Collections;
using Smash.StructsAndEnums;
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
		[SerializeField] protected PanelState _panelState;

		[Space(10)]
		[SerializeField] private ButtonSelectionHandler m_primaryButton;
		[SerializeField] protected BackButtonHandler _backButtonHandler;

		// protected PlayerInputActions _input;
		
		protected override void Awake()
		{
			base.Awake();
			// _input = new PlayerInputActions();
			gameObject.SetActive(false);
		}

		protected virtual IEnumerator SetSelected()
		{
			yield return null;
			EventSystem.current.SetSelectedGameObject(m_primaryButton.gameObject);
		}

		public virtual void OpenPanel()
		{
			_animationStrategy.Activate(OnComplete);
			
			/*_input.Player.Disable();
			_input.UI.Disable();
			_input.UI.Enable();
			
			_input.UI.Cancel.Disable();
			_input.UI.Retry.Disable();
			_input.UI.Resume.Disable();
			_input.UI.HorizontalScroll.Disable();
			_input.UI.VerticalScroll.Disable();
			_input.UI.Navigate.Enable();*/
		}
		
		public virtual void ClosePanel()
		{
			_animationStrategy.Deactivate();
		}
		
		private void OnComplete() => StartCoroutine(SetSelected());

		protected abstract void BackButtonPressed(InputAction.CallbackContext ctx);
	}
}

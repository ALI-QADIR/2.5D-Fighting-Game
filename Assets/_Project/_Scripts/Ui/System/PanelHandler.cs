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
		
		protected override void Awake()
		{
			base.Awake();
			_panelState = PanelState.SlidOutToRight;
			gameObject.SetActive(false);
		}

		protected virtual IEnumerator SetSelected()
		{
			yield return null;
			_panelState = PanelState.Open;
			EventSystem.current.SetSelectedGameObject(m_primaryButton.gameObject);
		}

		public virtual void OpenPanel()
		{
			_animationStrategy.Activate(OnComplete);
		}
		
		public virtual void ClosePanel()
		{
			_animationStrategy.Deactivate();
		}
		
		private void OnComplete() => StartCoroutine(SetSelected());

		protected virtual void BackButtonPressed(InputAction.CallbackContext _)
		{
			BackButtonPressed();
		}
		
		protected abstract void BackButtonPressed();
	}
}

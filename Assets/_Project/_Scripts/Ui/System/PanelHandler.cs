using System.Collections;
using PrimeTween;
using Smash.Ui.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Smash.Ui.System
{
	public abstract class PanelHandler : UiEventListener
	{
		[Header("Animation")] 
		[SerializeField] private float m_duration;
		[SerializeField] private Ease m_ease;
		[SerializeField] protected Transform _closeTransform;
		[SerializeField] protected Transform _openTransform;

		[Space(10)]
		[SerializeField] private ButtonSelectionHandler m_primaryButton;
		[SerializeField] protected BackButtonHandler _backButtonHandler;
		private GameObject m_lastSelected;

		protected Sequence _openSequence, _closeSequence;
		protected Transform _tr;

		protected PlayerInputActions _input;
		
		protected override void Awake()
		{
			base.Awake();
			_input = new PlayerInputActions();
			_tr = transform;
			_tr.position = _closeTransform.position;
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
			Show();
			
			ButtonSelectionHandler.OnButtonDeselected += OnButtonDeselected;
			
			_input.Player.Disable();
			_input.UI.Disable();
			_input.UI.Enable();
			
			_input.UI.Cancel.Disable();
			_input.UI.Navigate.Enable();
			_input.UI.Navigate.performed += OnNavigateStart;
		}
		
		public virtual void ClosePanel()
		{
			Hide();
			
			ButtonSelectionHandler.OnButtonDeselected -= OnButtonDeselected;
			
			_input.UI.Navigate.performed -= OnNavigateStart;
		}

		protected virtual void Show()
		{
			if (_openSequence.isAlive) return;
			
			gameObject.SetActive(true);
			_openSequence = Sequence.Create()
				.Group(Tween.Position(target: _tr, startValue: _closeTransform.position,
					endValue: _openTransform.position, duration: m_duration, ease: m_ease));
			
			_openSequence.OnComplete(target:this, target => target.OnComplete());
		}
		
		private void OnComplete() => StartCoroutine(SetSelected());

		protected virtual void Hide()
		{
			if (_closeSequence.isAlive) return;
			
			EventSystem.current.SetSelectedGameObject(null);
			
			_closeSequence = Sequence.Create()
				.Group(Tween.Position(target: _tr, startValue: _openTransform.position,
					endValue: _closeTransform.position, duration: m_duration, ease: m_ease));
			
			_closeSequence.OnComplete(target: this, target => target.DisableGameObject());
		}

		protected abstract void BackButtonPressed(InputAction.CallbackContext ctx);

		private void DisableGameObject() => gameObject.SetActive(false);
	}
}
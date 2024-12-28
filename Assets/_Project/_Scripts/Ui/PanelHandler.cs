using System.Collections;
using PrimeTween;
using Smash.System;
using TripleA.EventSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Smash.Ui
{
	public abstract class PanelHandler : EventListener<UiEventArgs>
	{
		[Header("Animation")] 
		[SerializeField] private float m_duration;
		[SerializeField] private Ease m_ease;
		[SerializeField] protected Transform _closeTransform;
		[SerializeField] protected Transform _openTransform;

		[Space(10)]
		[SerializeField] private ButtonSelectionHandler m_primaryButton;

		protected Sequence _openSequence, _closeSequence;
		protected Transform _tr;
		
		protected override void Awake()
		{
			base.Awake();
			_tr = transform;
			_tr.position = _closeTransform.position;
			gameObject.SetActive(false);
		}

		protected virtual IEnumerator Start()
		{
			yield return null;
			EventSystem.current.SetSelectedGameObject(m_primaryButton.gameObject);
		}

		public virtual void OpenPanel() => Show();
		public virtual void ClosePanel() => Hide();

		protected virtual void Show()
		{
			if (_openSequence.isAlive) return;
			
			gameObject.SetActive(true);
			_openSequence = Sequence.Create()
				.Group(Tween.Position(target: _tr, startValue: _closeTransform.position,
					endValue: _openTransform.position, duration: m_duration, ease: m_ease));
		}

		protected virtual void Hide()
		{
			if (_closeSequence.isAlive) return;
			
			_closeSequence = Sequence.Create()
				.Group(Tween.Position(target: _tr, startValue: _openTransform.position,
					endValue: _closeTransform.position, duration: m_duration, ease: m_ease));
			
			_closeSequence.OnComplete(target: this, target => target.DisableGameObject());
		}

		private void DisableGameObject() => gameObject.SetActive(false);
	}
}
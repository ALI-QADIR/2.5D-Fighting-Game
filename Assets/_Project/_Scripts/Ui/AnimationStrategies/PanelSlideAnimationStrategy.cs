using PrimeTween;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.AnimationStrategies
{
	public class PanelSlideAnimationStrategy : AnimationStrategy
	{
		[SerializeField] protected Transform _closeTransform, _openTransform;

		private Transform m_tr;
		
		protected void Awake()
		{
			m_tr = transform;
			m_tr.position = _closeTransform.position;
		}

		public override void Show()
		{
			if (_openSequence.isAlive) return;

			m_tr ??= transform;
			
			gameObject.SetActive(true);
			_openSequence = Sequence.Create()
				.Group(Tween.Position(target: m_tr, startValue: _closeTransform.position,
					endValue: _openTransform.position, duration: _duration, ease: _ease));

			_openSequence.OnComplete(target: this, target => target.onShowComplete());
		}

		public override void Hide()
		{
			if (_closeSequence.isAlive) return;

			m_tr ??= transform;
			
			_closeSequence = Sequence.Create()
				.Group(Tween.Position(target: m_tr, startValue: _openTransform.position,
					endValue: _closeTransform.position, duration: _duration, ease: _ease));
		
			_closeSequence.OnComplete(target: this, target => target.DisableGameObject());
		}
		
		private void DisableGameObject() => gameObject.SetActive(false);

		private void OnValidate()
		{
			m_tr = transform;
		}
	}
}

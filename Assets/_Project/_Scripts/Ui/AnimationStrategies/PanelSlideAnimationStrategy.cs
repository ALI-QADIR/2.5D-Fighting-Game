using System;
using PrimeTween;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.AnimationStrategies
{
	public class PanelSlideAnimationStrategy : AnimationStrategy<Action>
	{
		[SerializeField] protected Transform _closeTransform, _openTransform;

		private Transform m_tr;
		
		protected void Awake()
		{
			m_tr = transform;
			m_tr.position = _closeTransform.position;
		}

		public override void Activate(Action onCompleteAction = null)
		{
			if (_activateSequence.isAlive) return;

			m_tr ??= transform;
			
			gameObject.SetActive(true);
			_activateSequence = Sequence.Create()
				.Group(Tween.Position(target: m_tr, startValue: _closeTransform.position,
					endValue: _openTransform.position, duration: _duration, ease: _ease));

			_activateSequence.OnComplete(onCompleteAction);
		}

		public override void Deactivate(Action onCompleteAction = null)
		{
			if (_deactivateSequence.isAlive) return;

			m_tr ??= transform;
			
			_deactivateSequence = Sequence.Create()
				.Group(Tween.Position(target: m_tr, startValue: _openTransform.position,
					endValue: _closeTransform.position, duration: _duration, ease: _ease));
		
			_deactivateSequence.OnComplete(target: this, target => target.DisableGameObject());
		}
		
		private void DisableGameObject() => gameObject.SetActive(false);

		private void OnValidate()
		{
			m_tr = transform;
		}
	}
}

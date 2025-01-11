using PrimeTween;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.AnimationStrategies
{
	public class PanelSlideAnimationStrategy : AnimationStrategy
	{
		[SerializeField] protected Transform _closeTransform, _openTransform;
		
		protected override void Awake()
		{
			base.Awake();
			_tr.position = _closeTransform.position;
		}

		public override void Show()
		{
			if (_openSequence.isAlive) return;

			_tr ??= transform;
			
			gameObject.SetActive(true);
			_openSequence = Sequence.Create()
				.Group(Tween.Position(target: _tr, startValue: _closeTransform.position,
					endValue: _openTransform.position, duration: _duration, ease: _ease));

			_openSequence.OnComplete(target: this, target => target.onShowComplete());
		}

		public override void Hide()
		{
			if (_closeSequence.isAlive) return;

			_tr ??= transform;
			
			_closeSequence = Sequence.Create()
				.Group(Tween.Position(target: _tr, startValue: _openTransform.position,
					endValue: _closeTransform.position, duration: _duration, ease: _ease));
		
			_closeSequence.OnComplete(target: this, target => target.DisableGameObject());
		}
		
		private void DisableGameObject() => gameObject.SetActive(false);
	}
}

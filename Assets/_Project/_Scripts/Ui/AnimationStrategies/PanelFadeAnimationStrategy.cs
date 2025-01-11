using PrimeTween;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.UI;

namespace Smash.Ui.AnimationStrategies
{
	public class PanelFadeAnimationStrategy : AnimationStrategy
	{
		[SerializeField] private Image m_bgImage;

		private void Awake()
		{
			m_bgImage.color = Color.clear;
		}

		public override void Show()
		{
			if (_openSequence.isAlive) return;
			
			//TODO: Change From gray to white
			
			gameObject.SetActive(true);
			_openSequence = Sequence.Create()
				.Group(Tween.Color(target: m_bgImage, startValue: Color.clear,
					endValue: Color.gray, duration: _duration, ease: _ease));

			_openSequence.OnComplete(target: this, target => target.onShowComplete());
		}

		public override void Hide()
		{
			if (_closeSequence.isAlive) return;

			//TODO: Change From gray to white
			
			_closeSequence = Sequence.Create()
				.Group(Tween.Color(target: m_bgImage, startValue: Color.gray, 
					endValue: Color.clear, duration: _duration, ease: _ease));
		
			_closeSequence.OnComplete(target: this, target => target.DisableGameObject());
		}
		
		private void DisableGameObject() => gameObject.SetActive(false);
	}
}
using PrimeTween;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.UI;

namespace Smash.Ui.AnimationStrategies
{
	public class PanelFadeAnimationStrategy : AnimationStrategy
	{
		[SerializeField] private Image m_bgImage;
		[SerializeField] private Transform m_popupTransform;

		private void Awake()
		{
			m_bgImage.color = Color.clear;
			m_popupTransform.localScale = Vector3.zero;
		}

		public override void Show()
		{
			if (_openSequence.isAlive) return;
			
			//TODO: Change to white
			
			gameObject.SetActive(true);
			_openSequence = Sequence.Create()
				.Group(Tween.Color(target: m_bgImage, startValue: Color.clear,
					endValue: new Color(0, 0, 0.2f, 0.8f), duration: _duration, ease: _ease))
				.Group(Tween.Scale(target: m_popupTransform, startValue: Vector3.zero, endValue: Vector3.one,
					startDelay: _duration * 0.5f, duration: _duration * 0.5f, ease: _ease));

			_openSequence.OnComplete(target: this, target => target.onShowComplete());
		}

		public override void Hide()
		{
			if (_closeSequence.isAlive) return;

			//TODO: Change to white
			
			_closeSequence = Sequence.Create()
				.Group(Tween.Color(target: m_bgImage, startValue: new Color(0,0,0.2f,0.8f), 
					endValue: Color.clear, duration: _duration, ease: _ease))
				.Group(Tween.Scale(target: m_popupTransform, startValue: Vector3.one, endValue: Vector3.zero,
					duration: _duration * 0.5f, ease: _ease));
		
			_closeSequence.OnComplete(target: this, target => target.DisableGameObject());
		}
		
		private void DisableGameObject() => gameObject.SetActive(false);
	}
}
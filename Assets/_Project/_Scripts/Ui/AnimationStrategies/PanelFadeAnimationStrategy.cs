using System;
using PrimeTween;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.UI;

namespace Smash.Ui.AnimationStrategies
{
	public class PanelFadeAnimationStrategy : AnimationStrategy<Action>
	{
		[SerializeField] private Image m_bgImage;
		[SerializeField] private Transform m_popupTransform;

		private void Awake()
		{
			m_bgImage.color = Color.clear;
			m_popupTransform.localScale = Vector3.zero;
		}

		public override void Activate(Action onCompleteAction = null)
		{
			if (_activateSequence.isAlive) return;
			
			//TODO: Change to white
			
			gameObject.SetActive(true);
			_activateSequence = Sequence.Create()
				.Group(Tween.Color(target: m_bgImage, startValue: Color.clear,
					endValue: new Color(0, 0, 0.2f, 0.8f), duration: _duration, ease: _ease))
				.Group(Tween.Scale(target: m_popupTransform, startValue: Vector3.zero, endValue: Vector3.one,
					startDelay: _duration * 0.5f, duration: _duration * 0.5f, ease: _ease));

			_activateSequence.OnComplete(onCompleteAction);
		}

		public override void Deactivate(Action onCompleteAction = null)
		{
			if (_deactivateSequence.isAlive) return;

			//TODO: Change to white
			
			_deactivateSequence = Sequence.Create()
				.Group(Tween.Color(target: m_bgImage, startValue: new Color(0,0,0.2f,0.8f), 
					endValue: Color.clear, duration: _duration, ease: _ease))
				.Group(Tween.Scale(target: m_popupTransform, startValue: Vector3.one, endValue: Vector3.zero,
					duration: _duration * 0.5f, ease: _ease));
		
			_deactivateSequence.OnComplete(onCompleteAction);
		}
		
		private void DisableGameObject() => gameObject.SetActive(false);
	}
}

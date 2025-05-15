using System;
using PrimeTween;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Smash.Ui.AnimationStrategies
{
	public class ButtonFadeAnimationStrategy : AnimationStrategy<BaseEventData>
	{
		[SerializeField] private Image m_fadingImage;
		[SerializeField] private float m_scaleAmount = 1.1f;

		private static readonly Color m_S_InactiveColor = new (1, 1, 1, 0.0f);
		private static readonly Color m_S_ActiveColor = Color.white;
		private static Vector3 m_s_scale;
		private Transform m_tr;
		
		private void Awake()
		{
			m_s_scale = Vector3.one * m_scaleAmount;
			m_tr = transform;
			m_fadingImage.color = m_S_InactiveColor;
		}
		
		public override void Activate(BaseEventData onCompleteAction = null)
		{
			if (_deactivateSequence.isAlive) _deactivateSequence.Stop();
			// Todo: Sound
			_activateSequence = Sequence.Create()
				.Group(Tween.Color(m_fadingImage, m_S_InactiveColor, m_S_ActiveColor, _duration))
				.Group(Tween.Scale(target: m_tr, startValue: Vector3.one, endValue: m_s_scale, duration: _duration));
		}

		public override void Deactivate(BaseEventData onCompleteAction = null)
		{
			if (_activateSequence.isAlive) _activateSequence.Stop();
			
			_deactivateSequence = Sequence.Create()
				.Group(Tween.Color(m_fadingImage, m_S_ActiveColor, m_S_InactiveColor, _duration))
				.Group(Tween.Scale(target: m_tr, startValue: m_s_scale, endValue: Vector3.one, duration: _duration));
		}

		[RuntimeInitializeOnLoadMethod]
		private static void ScaleInitiator()
		{
			m_s_scale = Vector3.one;
		}
	}
}

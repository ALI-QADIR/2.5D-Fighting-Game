using System;
using PrimeTween;
using UnityEngine;

namespace Smash.Ui.System
{
	public abstract class AnimationStrategy : MonoBehaviour, IAnimationStrategy
	{
		[SerializeField] protected float _duration = 0.25f;
		[SerializeField] protected Ease _ease = Ease.OutSine;
		
		protected Sequence _openSequence, _closeSequence;
		
		public Action onShowComplete;
		public Action onHideComplete;

		public abstract void Show();
		
		public abstract void Hide();
	}
}

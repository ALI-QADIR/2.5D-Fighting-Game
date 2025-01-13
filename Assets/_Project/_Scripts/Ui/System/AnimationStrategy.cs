using System;
using PrimeTween;
using UnityEngine;

namespace Smash.Ui.System
{
	public abstract class AnimationStrategy<T> : MonoBehaviour, IAnimationStrategy<T>
	{
		[SerializeField] protected float _duration = 0.25f;
		[SerializeField] protected Ease _ease = Ease.OutSine;
		
		protected Sequence _activateSequence, _deactivateSequence;

		public abstract void Activate(T onCompleteAction = default);
		
		public abstract void Deactivate(T onCompleteAction = default);
	}
}

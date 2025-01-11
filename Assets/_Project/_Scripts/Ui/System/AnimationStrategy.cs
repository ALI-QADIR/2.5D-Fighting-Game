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
		protected Transform _tr;
		
		public Action onShowComplete;
		public Action onHideComplete;
		
		protected virtual void Awake()
		{
			_tr ??= transform;
		}

		public abstract void Show();
		
		public abstract void Hide();

		private void OnValidate()
		{
			_tr = transform;
		}
	}
}

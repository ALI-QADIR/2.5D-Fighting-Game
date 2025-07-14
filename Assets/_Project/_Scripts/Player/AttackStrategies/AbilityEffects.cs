using System;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	[Serializable]
	public abstract class AbilityEffect
	{
		[field: SerializeField] public bool IsSelfEffect { get; private set; }
		public float HeldDuration { protected get; set; }
		protected float _modifier;
		
		public abstract void Execute(Collider collider);
		protected virtual void ModifyEffect(){}
	}
	

	[Serializable]
	public class DamageEffect : AbilityEffect
	{
		[SerializeField] private int m_maxDamage = 10;
		[SerializeField] private AnimationCurve m_damageCurve;

		public override void Execute(Collider collider)
		{
			ModifyEffect();
			if (collider.TryGetComponent<CharacterHealth>(out var health))
				health.TakeDamage(m_maxDamage * _modifier);
		}

		protected override void ModifyEffect()
		{
			_modifier = m_damageCurve.Evaluate(HeldDuration / 3f); // TODO: remove magic numbers
		}
	}

	[Serializable]
	public class KnockBackEffect : AbilityEffect
	{
		[SerializeField] private float m_force;

		public override void Execute(Collider collider)
		{
			Debug.Log(m_force);
		}
	}

	[Serializable]
	public class StunEffect : AbilityEffect
	{
		[SerializeField] private float m_stunDuration;
		public override void Execute(Collider collider)
		{
			Debug.Log("Stun");
		}
	}

	[Serializable]
	public class SlowEffect : AbilityEffect
	{
		[SerializeField] private float m_slowDuration;
		[SerializeField, Range(0, 1)] private float m_slowMultiplier;
		
		public override void Execute(Collider collider)
		{
			Debug.Log("Slow");
		}
	}

	[Serializable]
	public class NoEffect : AbilityEffect
	{
		public override void Execute(Collider collider)
		{
			Debug.Log("No effect");
		}
	}
}

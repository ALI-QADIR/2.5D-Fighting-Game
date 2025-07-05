using System;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	[Serializable]
	public abstract class AbilityEffect
	{
		public abstract void Execute(Collider collider);
	}
	

	[Serializable]
	public class DamageEffect : AbilityEffect
	{
		public int damage;

		public override void Execute(Collider collider)
		{
			if (collider.TryGetComponent<CharacterHealth>(out var health))
				health.TakeDamage(10);
		}
	}
}

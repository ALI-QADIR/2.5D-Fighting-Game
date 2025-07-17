using System.Collections.Generic;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	public class AbilityContext
	{
		public List<AbilityEffect> otherAbilityEffects = new();
		public List<AbilityEffect> selfAbilityEffects = new();
		private HashSet<Collider> m_hits = new();
		public Collider ownerCollider;

		public void ClearHashSet()
		{
			m_hits.Clear();
		}
		
		public void ApplyEffects(Collider result)
		{
			if (m_hits.Contains(result)) return;
			foreach (var abilityEffect in otherAbilityEffects)
			{
				abilityEffect.Execute(result);
			}
			m_hits.Add(result);
		}

		public void ApplySelfEffects()
		{
			foreach (var abilityEffect in selfAbilityEffects)
			{
				abilityEffect.Execute(ownerCollider);
			}
		}
		
		public void SetAbilityEffects(List<AbilityEffect> abilityEffectsList)
		{
			foreach (var abilityEffect in abilityEffectsList)
			{
				if (abilityEffect.IsSelfEffect)
				{
					selfAbilityEffects.Add(abilityEffect);
					continue;
				}
				otherAbilityEffects.Add(abilityEffect);
			}
		}
		
		public void SetAbilityModifier(float heldTime)
		{
			foreach (var abilityEffect in otherAbilityEffects)
			{
				abilityEffect.HeldDuration = heldTime;
			}

			foreach (var abilityEffect in selfAbilityEffects)
			{
				abilityEffect.HeldDuration = heldTime;
			}
		}
	}
}

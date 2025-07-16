using System.Collections.Generic;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	public class AbilityContext
	{
		public List<AbilityEffect> abilityEffects;
		private HashSet<Collider> m_hits;
		public Collider ownerCollider;

		public AbilityContext()
		{
			m_hits = new HashSet<Collider>();
		}
		
		public void ClearHashSet()
		{
			m_hits.Clear();
		}
		
		public void ApplyEffects(Collider result)
		{
			if (m_hits.Contains(result)) return;
			foreach (var abilityEffect in abilityEffects)
			{
				if (abilityEffect.IsSelfEffect)
				{
					abilityEffect.Execute(ownerCollider);
					continue;
				}
				abilityEffect.Execute(result);
			}
			m_hits.Add(result);
		}
		
		public void SetAbilityModifier(float heldTime)
		{
			foreach (var abilityEffect in abilityEffects)
			{
				abilityEffect.HeldDuration = heldTime;
			}
		}
	}
}

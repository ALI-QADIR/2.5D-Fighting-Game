using System.Collections.Generic;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	public class AbilityContext
	{
		private readonly List<AbilityEffect> abilityEffects;
		private HashSet<Collider> m_hits;

		public AbilityContext(List<AbilityEffect> abilityEffects)
		{
			this.abilityEffects = abilityEffects;
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

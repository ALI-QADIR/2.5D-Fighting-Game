using System.Collections.Generic;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	public class AbilityStrategy
	{
		public Scanner scanner;

		private readonly AbilityContext m_abilityContext = new();

		public bool CanScan { get; set; }

		public void OnEnter()
		{
			m_abilityContext.ClearHashSet();
		}

		public void OnFixedUpdate()
		{
			if (!CanScan) return;
			
			scanner.Scan();
		}

		public void Attack()
		{
			scanner.Emit();
			m_abilityContext.ApplySelfEffects();
		}

		public void OnExit()
		{
		}
		
		public void SetAbilityContextInScanner()
		{
			if (!scanner) return;
			scanner.context = m_abilityContext;
		}
		
		public void SetOwningCollider(Collider collider)
		{
			if (m_abilityContext == null) return;
			m_abilityContext.ownerCollider = collider;
		}
		
		public void SetAbilityEffectsInContext(List<AbilityEffect> abilityEffectsList)
		{
			m_abilityContext.SetAbilityEffects(abilityEffectsList);
		}

		public void SetAbilityModifier(float heldTime)
		{
			m_abilityContext.SetAbilityModifier(heldTime);
		}
	}
}

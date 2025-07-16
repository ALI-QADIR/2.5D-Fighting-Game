using System.Collections.Generic;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	public class AbilityStrategy
	{
		public Scanner scanner;

		private AbilityContext m_abilityContext;

		public bool CanAttack { get; set; }

		public AbilityStrategy()
		{
			m_abilityContext = new AbilityContext();
		}

		public void OnEnter()
		{
			m_abilityContext.ClearHashSet();
		}

		public void OnFixedUpdate()
		{
			if (!CanAttack) return;
			
			scanner.Scan();
		}

		public void Attack()
		{
			scanner.Emit();
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
		
		public void SetAbilityContext(List<AbilityEffect> abilityEffectsList)
		{
			m_abilityContext.abilityEffects = abilityEffectsList;
		}

		public void SetAbilityModifier(float heldTime)
		{
			m_abilityContext.SetAbilityModifier(heldTime);
		}
	}
}

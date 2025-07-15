using System.Collections.Generic;
using Smash.Player.Components;

namespace Smash.Player.AbilityStrategies
{
	public class AbilityStrategy
	{
		public Scanner scanner;

		private AbilityContext m_abilityContext;

		public bool CanAttack { get; set; }

		public void OnEnter()
		{
			m_abilityContext.Clear();
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
			if (m_abilityContext == null) return;
			scanner.context = m_abilityContext;
		}
		
		public void CreateAbilityContext(List<AbilityEffect> abilityEffectsList)
		{
			m_abilityContext = new AbilityContext(abilityEffectsList);
			SetAbilityContextInScanner();
		}

		public void SetAbilityModifier(float heldTime)
		{
			m_abilityContext.SetAbilityModifier(heldTime);
		}
	}
}

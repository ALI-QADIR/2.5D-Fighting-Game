using System.Collections.Generic;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	public static class AbilityStrategyFactory
	{
		public static AbilityStrategy CreateAttackStrategy()
		{
			return new AbilityStrategy();
		}

		public static AbilityStrategy WithScanner(this AbilityStrategy strategy, ScanningStrategy scanningStrategy, Transform parent, int targetLayer = Physics.AllLayers)
		{
			strategy.scanner = scanningStrategy.CreateScanner(parent)
				.WithOffset(scanningStrategy.CenterOffset)
				.WithTargetLayer(targetLayer);
			strategy.SetAbilityContextInScanner();
			return strategy;
		}
		
		public static AbilityStrategy WithAbilityEffect(this AbilityStrategy strategy, List<AbilityEffect> abilityEffects)
		{
			strategy.SetAbilityContext(abilityEffects);
			return strategy;
		}
		
		public static AbilityStrategy WithOwningCollider(this AbilityStrategy strategy, Collider collider)
		{
			strategy.SetOwningCollider(collider);
			return strategy;
		}
	}
}

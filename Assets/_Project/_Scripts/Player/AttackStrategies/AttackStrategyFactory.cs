using System.Collections.Generic;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	public static class AttackStrategyFactory
	{
		public static AttackStrategy CreateAttackStrategy()
		{
			return new AttackStrategy();
		}

		public static AttackStrategy WithScanner(this AttackStrategy strategy, ScanningStrategy scanningStrategy, Transform parent, int targetLayer = Physics.AllLayers)
		{
			strategy.scanner = ScannerFactory.CreateScanner(scanningStrategy.scannerPrefab, parent)
				.WithOffset(scanningStrategy.centerOffset)
				.WithTargetLayer(targetLayer)
				.WithDimensions(scanningStrategy.dimensions);
			return strategy;
		}
		
		public static AttackStrategy WithAbilityEffect(this AttackStrategy strategy, List<AbilityEffect> abilityEffects)
		{
			strategy.abilityEffects = abilityEffects;
			return strategy;
		}
	}
}

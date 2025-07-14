using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	[Serializable]
	public class AttackStrategyData
	{
		[field: SerializeField] public float AnimDuration { get; private set; } 
		[field: SerializeReference] public ScanningStrategy ScanningStrategy { get; private set; }
		[field: SerializeReference] public List<AbilityEffect> AbilityEffects{ get; private set; }
	}
}

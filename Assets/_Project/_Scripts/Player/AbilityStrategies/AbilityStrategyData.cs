using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	[Serializable]
	public class AbilityStrategyData
	{
		[field: SerializeField] public float AnimDuration { get; private set; } 
		[field: SerializeReference] public ScanningStrategy ScanningStrategy { get; private set; }
		[field: SerializeReference] public List<AbilityEffect> AbilityEffects{ get; private set; }
	}
}

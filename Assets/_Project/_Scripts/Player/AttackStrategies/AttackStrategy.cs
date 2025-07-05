using System;
using System.Collections.Generic;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	public class AttackStrategy : IAttackStrategy
	{
		public Scanner scanner;
		public List<AbilityEffect> abilityEffects = new();

		private HashSet<Collider> m_hits = new();

		public bool CanAttack { get; set; }

		public void OnEnter()
		{
			Debug.Log("OnEnter");
			m_hits.Clear();
		}

		public void OnFixedUpdate()
		{
			if (!CanAttack) return;
			
			int hitCount = scanner.Cast();
			Debug.Log(hitCount);
			
			for (int i = 0; i < hitCount; i++)
			{
				var result = scanner.results[i];
				if (result != null && !m_hits.Contains(result))
				{
					ApplyEffects(result);
				}
			}
		}

		public void Attack()
		{
		}

		public void OnExit()
		{
		}

		private void ApplyEffects(Collider result)
		{
			foreach (var effect in abilityEffects)
			{
				effect.Execute(result);
			}
			m_hits.Add(result);
		}
	}
	
	[Serializable]
	public class AttackStrategyData
	{
		[field: SerializeField] public float AnimDuration { get; private set; } 
		[field: SerializeReference] public ScanningStrategy ScanningStrategy { get; private set; }
		[field: SerializeReference] public List<AbilityEffect> AbilityEffects{ get; private set; }
	}
}

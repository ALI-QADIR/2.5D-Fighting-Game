using System.Collections.Generic;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	public class XbotMainAttackStrategy : AttackStrategy, IAttackStrategy
	{
		[SerializeField] private Transform m_center;
		[SerializeField] private Vector3 m_halfExtents;
		private HurtBox m_hurtBox;
		private HashSet<Collider> m_hits = new ();

		public override void Initialise(int layerMask)
		{
			m_hurtBox = new HurtBox();
			m_hurtBox.layerMask = layerMask;
		}

		public override void OnBegin()
		{
			m_hits.Clear();
		}
		
		public override void OnFixedUpdate()
		{
			if (!CanAttack) return;
			
			int hitCount = m_hurtBox.Cast(m_center.position, m_halfExtents, m_center.rotation);
			
			for (int i = 0; i < hitCount; i++)
			{
				var result = m_hurtBox.results[i];
				if (!m_hits.Contains(result))
				{
					Debug.Log(result.gameObject.name);
					result.TryGetComponent<CharacterHealth>(out var health);
					health.TakeDamage(10);
					m_hits.Add(result);
				}
			}
		}

		public override void Attack()
		{
		}
	}
}

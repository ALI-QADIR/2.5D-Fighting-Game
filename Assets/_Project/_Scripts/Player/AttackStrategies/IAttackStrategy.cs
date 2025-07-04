using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	public interface IAttackStrategy
	{
		public bool CanAttack { get; set; }
		void OnEnter();
		void OnFixedUpdate();
		void OnExit();
		void Attack();
	}
}

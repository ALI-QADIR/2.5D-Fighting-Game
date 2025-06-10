using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	public interface IAttackStrategy
	{
		public bool CanAttack { get; set; }
		void OnBegin();
		void OnFixedUpdate();
		void Attack();
	}

	public abstract class AttackStrategy : MonoBehaviour, IAttackStrategy
	{
		public abstract void Initialise(int layerMask);
		public bool CanAttack { get; set; }
		public abstract void OnBegin();
		public abstract void OnFixedUpdate();
		public abstract void Attack();
	}
}

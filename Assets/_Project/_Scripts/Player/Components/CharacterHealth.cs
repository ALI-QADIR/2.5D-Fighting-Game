using UnityEngine;

namespace Smash.Player.Components
{
	public class CharacterHealth : MonoBehaviour
	{
		[field: SerializeField] public float Health { get; private set; }

		public void TakeDamage(float damage)
		{
			Health += damage;
			// Debug.Log($"Health: {Health}");
		}
	}
}

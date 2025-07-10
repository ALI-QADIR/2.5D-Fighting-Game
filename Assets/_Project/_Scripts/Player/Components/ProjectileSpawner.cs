using UnityEngine;

namespace Smash.Player.Components
{
	public class ProjectileSpawner : SphereHurtBox
	{
		[ReadOnly] public float speed;
		[ReadOnly] public float maxDistance;
		[ReadOnly] public Projectile projectilePrefab;
		
		public override int Scan()
		{
			Debug.Log("Projectile");
			return 0;
		}

		public override void Emit()
		{
			var _ = Instantiate(projectilePrefab, transform.position, transform.rotation);
			_.speed = speed;
			_.maxDistance = maxDistance;
			_.spawner = this;
		}
	}
}

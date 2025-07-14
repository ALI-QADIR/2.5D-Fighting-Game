using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player
{
	public class Projectile : MonoBehaviour
	{
		[ReadOnly] public float speed;
		[ReadOnly] public float maxDistance;
		[ReadOnly] public ProjectileSpawner spawner;

		private float m_distanceTravelled;

		private void Update()
		{
			transform.position += transform.right * (speed * Time.deltaTime);
			m_distanceTravelled += Time.deltaTime * speed;
			if (m_distanceTravelled >= maxDistance)
				Destroy(gameObject);
		}

		private void FixedUpdate()
		{
			int hits = Physics.OverlapSphereNonAlloc(
				transform.position, 
				spawner.radius, 
				spawner.results,
				spawner.layerMask);
			
			spawner.OnScan(hits);
		}
	}
}

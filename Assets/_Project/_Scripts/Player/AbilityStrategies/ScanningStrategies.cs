using System;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	[Serializable]
	public abstract class ScanningStrategy
	{
		[field: SerializeField] public Vector3 CenterOffset {get; private set;}

		public abstract Scanner CreateScanner(Transform parent);
	}
	
	[Serializable]
	public class ProjectileScanningStrategy : ScanningStrategy
	{
		[field: SerializeReference] private ProjectileSpawner m_prefab;
		[field: SerializeField] private float m_radius;
		[SerializeField] private float m_projectileSpeed;
		[SerializeField] private float m_maxDistance;
		[SerializeReference] private Projectile m_projectilePrefab; 

		public override Scanner CreateScanner(Transform parent)
		{
			return ScannerFactory.CreateScanner(m_prefab, parent)
				.WithSpeedAndMaxDistance(m_projectileSpeed, m_maxDistance)
				.WithProjectilePrefab(m_projectilePrefab)
				.WithRadius(m_radius);
		}
	}
	
	[Serializable]
	public class BoxScanningStrategy : ScanningStrategy
	{
		[field: SerializeReference] private CubeHurtBox m_prefab;
		[field: SerializeField] private Vector3 m_halfExtents;
		
		public override Scanner CreateScanner(Transform parent)
		{
			return ScannerFactory.CreateScanner(m_prefab, parent)
				.WithHalfExtents(m_halfExtents);
		}
	}
	
	[Serializable]
	public class SphereScanningStrategy : ScanningStrategy
	{
		[field: SerializeReference] private SphereHurtBox m_prefab;
		[field: SerializeField] private float m_radius;
		public override Scanner CreateScanner(Transform parent)
		{
			return ScannerFactory.CreateScanner(m_prefab, parent)
				.WithRadius(m_radius);
		}
	}
	
	[Serializable]
	public class NullScanningStrategy : ScanningStrategy
	{
		[field: SerializeReference] private NullScanner m_prefab;

		public override Scanner CreateScanner(Transform parent)
		{
			return ScannerFactory.CreateScanner(m_prefab, parent);
		}
	}
}

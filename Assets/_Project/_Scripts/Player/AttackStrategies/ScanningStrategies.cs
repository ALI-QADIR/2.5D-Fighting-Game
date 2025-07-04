using System;
using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.AttackStrategies
{
	[Serializable]
	public abstract class ScanningStrategy
	{
		[SerializeReference] public Scanner scannerPrefab;
		public Vector3 centerOffset;
		public Vector3 dimensions;
	}
	
	[Serializable]
	public class BoxScanningStrategy : ScanningStrategy
	{
	}
	
	[Serializable]
	public class SphereScanningStrategy : ScanningStrategy
	{
		// public float dimensions;
	}
}

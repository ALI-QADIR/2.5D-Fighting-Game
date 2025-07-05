using UnityEngine;

namespace Smash.Player.Components
{
	public static class ScannerFactory
	{
		public static T CreateScanner<T>(T original, Transform parent) where T : Scanner
		{
			return Object.Instantiate(original, parent);
		}

		public static Scanner WithOffset(this Scanner scanner, Vector3 offset)
		{
			scanner.transform.localPosition += offset;
			return scanner;
		}
		
		public static Scanner WithTargetLayer(this Scanner scanner, int layerMask)
		{
			scanner.layerMask = layerMask;
			return scanner;
		}
		
		public static Scanner WithHalfExtents(this CubeHurtBox scanner, Vector3 halfExtents)
		{
			scanner.transform.localScale = halfExtents * 2;
			scanner.halfExtents = halfExtents;
			return scanner;
		}

		public static Scanner WithRadius(this SphereHurtBox scanner, float radius)
		{
			scanner.transform.localScale = Vector3.one * radius;
			scanner.radius = radius;
			return scanner;
		}
	}
}
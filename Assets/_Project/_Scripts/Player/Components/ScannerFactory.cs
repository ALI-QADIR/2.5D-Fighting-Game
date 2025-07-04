using UnityEngine;

namespace Smash.Player.Components
{
	public static class ScannerFactory
	{
		public static Scanner CreateScanner(Scanner original, Transform parent)
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
		
		public static Scanner WithDimensions(this Scanner scanner, Vector3 halfExtents)
		{
			scanner.transform.localScale = halfExtents * 2;
			scanner.dimensions = halfExtents;
			return scanner;
		}

		public static Scanner WithDimensions(this Scanner scanner, float radius)
		{
			scanner.dimensions = new Vector3(radius, radius, radius);
			return scanner;
		}
	}
}
using UnityEngine;

namespace Smash.Player.Components
{
	public abstract class Scanner : MonoBehaviour
	{
		public Collider[] results = new Collider[6];
		public int layerMask;
		public Vector3 dimensions;

		public abstract int Cast();
	}
}

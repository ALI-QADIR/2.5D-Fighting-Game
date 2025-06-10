using UnityEngine;

namespace Smash.Player.Components
{
	public class HurtBox
	{
		public Collider[] results = new Collider[6];
		public int layerMask;

		public int Cast(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			// Debug.Log("OverlapBox");
			return Physics.OverlapBoxNonAlloc(
				center: center, 
				halfExtents: halfExtents,
				orientation: orientation,
				mask: layerMask,
				results: results
			);
		}
	}
}

using UnityEngine;

namespace Smash.Player.Components
{
	public class CubeHurtBox : Scanner
	{
		public Vector3 halfExtents;
		
		public override int Cast()
		{
			Debug.Log("OverlapBox");
			return Physics.OverlapBoxNonAlloc(
				center: transform.position, 
				halfExtents: halfExtents, 
				orientation: transform.rotation,
				mask: layerMask,
				results: results);
		}
	}
}

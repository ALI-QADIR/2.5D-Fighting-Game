using UnityEngine;

namespace Smash.Player.Components
{
	public class CubeHurtBox : Scanner
	{
		public override int Cast()
		{
			Debug.Log("OverlapBox");
			return Physics.OverlapBoxNonAlloc(
				center: transform.position, 
				halfExtents: dimensions, 
				orientation: transform.rotation,
				mask: layerMask,
				results: results);
		}
	}
}

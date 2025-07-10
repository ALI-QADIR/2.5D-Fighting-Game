using UnityEngine;

namespace Smash.Player.Components
{
	public class SphereHurtBox : Scanner
	{
		[ReadOnly] public float radius;
		
		public override int Scan()
		{
			Debug.Log("OverlapSphere");
			return Physics.OverlapSphereNonAlloc(position: transform.position, 
				radius: radius, 
				results: results, 
				layerMask: layerMask);
		}

		public override void Emit()
		{
			// no-op
		}
	}
}

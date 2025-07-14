using UnityEngine;

namespace Smash.Player.Components
{
	public class SphereHurtBox : Scanner
	{
		[ReadOnly] public float radius;
		
		public override void Scan()
		{
			Debug.Log("OverlapSphere");
			int hits = Physics.OverlapSphereNonAlloc(position: transform.position, 
				radius: radius, 
				results: results, 
				layerMask: layerMask);
			
			OnScan(hits);
		}

		public override void Emit()
		{
			// no-op
		}
	}
}

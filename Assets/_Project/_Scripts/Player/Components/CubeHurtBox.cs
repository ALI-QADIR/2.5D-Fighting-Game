using UnityEngine;

namespace Smash.Player.Components
{
	public class CubeHurtBox : Scanner
	{
		[ReadOnly] public Vector3 halfExtents;
		
		public override void Scan()
		{
			Debug.Log("OverlapBox");
			int hits = Physics.OverlapBoxNonAlloc(
				center: transform.position, 
				halfExtents: halfExtents, 
				orientation: transform.rotation,
				mask: layerMask,
				results: results);
			
			OnScan(hits);
		}

		public override void Emit()
		{
			// no-op
		}
	}
}

using UnityEngine;

namespace Smash.Player.Components
{
	public abstract class Scanner : MonoBehaviour
	{
		[ReadOnly] public Collider[] results = new Collider[6];
		[HideInInspector] public int layerMask;

		public abstract int Cast();
	}
}

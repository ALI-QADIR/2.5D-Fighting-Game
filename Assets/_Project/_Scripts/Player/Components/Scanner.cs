using Smash.Player.AbilityStrategies;
using UnityEngine;

namespace Smash.Player.Components
{
	public abstract class Scanner : MonoBehaviour
	{
		[ReadOnly] public Collider[] results = new Collider[6];
		[HideInInspector] public int layerMask;
		public AbilityContext context;

		public abstract void Scan();

		public void OnScan(int hits)
		{
			for (int i = 0; i < hits; i++)
			{
				var result = results[i];
				if (result)
				{
					context.ApplyEffects(result);
				}
			}
		}

		public abstract void Emit();
	}
}

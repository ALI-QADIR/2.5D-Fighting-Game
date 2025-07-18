using UnityEngine;

namespace Smash
{
	public class TestPlayer : MonoBehaviour
	{
		private void Start()
		{
			gameObject.layer = LayerMask.NameToLayer("TestPlayer");
		}
	}
}

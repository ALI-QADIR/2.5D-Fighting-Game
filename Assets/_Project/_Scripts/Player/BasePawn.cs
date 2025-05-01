using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(InputHandler))]
	public class BasePawn : MonoBehaviour
	{
		// public InputHandler InputHandler { get; set; }

		public abstract void Initialise();

		/*public InputHandler GetInputHandler()
		{
			_inputHandler ??= gameObject.GetOrAddComponent<InputHandler>();
			return _inputHandler;
		}*/
	}
}

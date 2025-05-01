using TripleA.Utils.Extensions;
using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(InputHandler))]
	public class BasePawn : MonoBehaviour
	{
		private BaseController m_possessingController;
		protected InputHandler _inputHandler;

		public virtual void Initialise(BaseController possessingController)
		{
			m_possessingController = possessingController;
			_inputHandler ??= gameObject.GetOrAddComponent<InputHandler>();
		}

		public InputHandler GetInputHandler() => _inputHandler;
	}
}

using Smash.Player.CommandPattern;
using Smash.Player.Input;
using UnityEngine;

namespace Smash.Player
{
	public abstract class BasePawn : MonoBehaviour
	{
		private BaseController m_possessingController;

		public virtual void Initialise(BaseController possessingController)
		{
			m_possessingController = possessingController;
		}
	}
}

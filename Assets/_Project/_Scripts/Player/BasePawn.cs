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

		public abstract void HandleUpInput();
		public abstract void HandleDownInput();
		public abstract void HandleLeftInput();
		public abstract void HandleRightInput();
		public abstract void HandleJumpInput();
		public abstract void HandleMainAttackInput();
		public abstract void HandleSideMainAttackInput();
		public abstract void HandleUpMainAttackInput();
		public abstract void HandleDownMainAttackInput();
		public abstract void HandleSpecialAttackInput();
		public abstract void HandleSideSpecialAttackInput();
		public abstract void HandleUpSpecialAttackInput();
		public abstract void HandleDownSpecialAttackInput();
		public abstract void HandleDpadNullInput();
	}
}

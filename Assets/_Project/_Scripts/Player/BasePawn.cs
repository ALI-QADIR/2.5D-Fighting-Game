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
		public abstract void HandleMainAttackInputStart();
		public abstract void HandleMainAttackInputEnd(float heldTime);
		public abstract void HandleSideMainAttackInputStart();
		public abstract void HandleSideMainAttackInputEnd(float heldTime);
		public abstract void HandleUpMainAttackInputStart();
		public abstract void HandleUpMainAttackInputEnd(float heldTime);
		public abstract void HandleDownMainAttackInputStart();
		public abstract void HandleDownMainAttackInputEnd(float heldTime);
		public abstract void HandleSpecialAttackInput();
		public abstract void HandleSideSpecialAttackInput();
		public abstract void HandleUpSpecialAttackInput();
		public abstract void HandleDownSpecialAttackInput();
		public abstract void HandleDpadNullInput();
	}
}

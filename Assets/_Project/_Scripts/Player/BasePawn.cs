using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player
{
	public abstract class BasePawn : MonoBehaviour, IInputHandler
	{
		public abstract void Initialise();
		public abstract void SetIndex(int index);
		
		public int PlayerIndex { get; set; }
		
		public virtual void HandleRightInput() {}
		public virtual void HandleLeftInput() {}
		public virtual void HandleDpadNullInput() {}
		public virtual void HandleUpInput() {}
		public virtual void HandleDownInput() {}
		public virtual void HandleJumpInput() {}
		public virtual void HandleMainAttackInputStart() {}
		public virtual void HandleMainAttackInputEnd(float heldTime) {}
		public virtual void HandleSideMainAttackInputStart(int direction) {}
		public virtual void HandleSideMainAttackInputEnd(float heldTime, int direction) {}
		public virtual void HandleUpMainAttackInputStart() {}
		public virtual void HandleUpMainAttackInputEnd(float heldTime) {}
		public virtual void HandleDownMainAttackInputStart() {}
		public virtual void HandleDownMainAttackInputEnd(float heldTime) {}
		public virtual void HandleSpecialAttackInputStart() {}
		public virtual void HandleSpecialAttackInputEnd(float heldTime) {}
		public virtual void HandleSideSpecialAttackInputStart(int direction) {}
		public virtual void HandleSideSpecialAttackInputEnd(float heldTime, int direction) {}
		public virtual void HandleDownSpecialAttackInputStart() {}
		public virtual void HandleDownSpecialAttackInputEnd(float heldTime) {}
		public virtual void HandleUpSpecialAttackInputStart() {}
		public virtual void HandleUpSpecialAttackInputEnd(float heldTime) {}
		
		public virtual void HandleCancelButton() {}

		public virtual void HandleCancelButton(IGameplayActionCommand command) {}

		public virtual void HandleSubmitButton(IGameplayActionCommand command) {}

		public virtual void HandleHorizontalScrollInput(IGameplayActionCommand command) {}

		public virtual void HandleVerticalScrollInput(IGameplayActionCommand command) {}

		public virtual void HandleShoulderTriggerInput(IGameplayActionCommand command) {}

		public virtual void HandleShoulderButtonInput(IGameplayActionCommand command) {}

		public virtual void HandleResumeInput(IGameplayActionCommand command) {}
	}
}

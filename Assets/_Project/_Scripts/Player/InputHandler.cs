using Smash.Player.States;
using UnityEngine;

namespace Smash.Player
{
	public class InputHandler : MonoBehaviour
	{
		private CharacterPawn m_charPawn;
		private UiPawn m_uiPawn;
		
		public void SetCharacterPawn(CharacterPawn charPawn)
		{
			m_charPawn = charPawn;
		}

		public void SetUiPawn(UiPawn uiPawn)
		{
			m_uiPawn = uiPawn;
		}

		#region Character Pawn Input

		public void HandleRightInput() => m_charPawn.Direction = Vector3.right;

		public void HandleLeftInput() => m_charPawn.Direction = Vector3.left;
		
		public void HandleDpadNullInput() => m_charPawn.Direction = Vector3.zero;

		public void HandleUpInput()
		{
			if (m_charPawn.CurrentState is Ledge)
			{
				m_charPawn.HandleClimb();
				return;
			}
			// else if (CurrentState is WallSlide)
			// {
			//  wall jump
			//  return;
			// }
			m_charPawn.HandleJumpInput();
		}

		public void HandleDownInput()
		{
			// if (m_motor.IsGrounded())
			// {
			// pass through platform
			// return;
			// }
		}
		
		public void HandleJumpInput() => m_charPawn.HandleJumpInput();

		public void HandleMainAttackInputStart()
		{
			m_charPawn.CurrentStateMachine.MainAttackHold = true;
		}

		public void HandleMainAttackInputEnd(float heldTime)
		{
			m_charPawn.CurrentStateMachine.MainAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			m_charPawn.CurrentStateMachine.MainAttackHold = false;
		}
		
		public void HandleSideMainAttackInputStart()
		{
			m_charPawn.CurrentStateMachine.SideMainAttackHold = true;
		}
		
		public void HandleSideMainAttackInputEnd(float heldTime)
		{
			m_charPawn.CurrentStateMachine.SideMainAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			m_charPawn.CurrentStateMachine.SideMainAttackHold = false;
		}
		
		public void HandleUpMainAttackInputStart()
		{
			m_charPawn.CurrentStateMachine.UpMainAttackHold = true;
		}
		
		public void HandleUpMainAttackInputEnd(float heldTime)
		{
			m_charPawn.CurrentStateMachine.UpMainAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			m_charPawn.CurrentStateMachine.UpMainAttackHold = false;
		}

		public void HandleDownMainAttackInputStart()
		{
			m_charPawn.CurrentStateMachine.DownMainAttackHold = true;
		}

		public void HandleDownMainAttackInputEnd(float heldTime)
		{
			m_charPawn.CurrentStateMachine.DownMainAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			m_charPawn.CurrentStateMachine.DownMainAttackHold = false;
		}

		public void HandleSpecialAttackInputStart()
		{
			Debug.Log("Special Attack Input Start");
			m_charPawn.CurrentStateMachine.SpecialAttackHold = true;
		}

		public void HandleSpecialAttackInputEnd(float heldTime)
		{
			Debug.Log("Special Attack Input End");
			m_charPawn.CurrentStateMachine.SpecialAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			m_charPawn.CurrentStateMachine.SpecialAttackHold = false;
		}

		public void HandleSideSpecialAttackInputStart()
		{
			m_charPawn.CurrentStateMachine.SideSpecialAttackHold = true;
		}

		public void HandleSideSpecialAttackInputEnd(float heldTime)
		{
			m_charPawn.CurrentStateMachine.SideSpecialAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			m_charPawn.CurrentStateMachine.SideSpecialAttackHold = false;
		}

		public void HandleDownSpecialAttackInputStart()
		{
			m_charPawn.CurrentStateMachine.DownSpecialAttackHold = true;
		}
		
		public void HandleDownSpecialAttackInputEnd(float heldTime)
		{
			m_charPawn.CurrentStateMachine.DownSpecialAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			m_charPawn.CurrentStateMachine.DownSpecialAttackHold = false;
		}
		
		public void HandleUpSpecialAttackInputStart()
		{
			m_charPawn.CurrentStateMachine.UpSpecialAttackHold = true;
		}
		
		public void HandleUpSpecialAttackInputEnd(float heldTime)
		{
			m_charPawn.CurrentStateMachine.UpSpecialAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			m_charPawn.CurrentStateMachine.UpSpecialAttackHold = false;
		}

		#endregion Character Pawn Input
	}
}

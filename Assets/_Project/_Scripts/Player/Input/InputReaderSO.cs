using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Smash.Player.Input
{
	[CreateAssetMenu(menuName = "Scriptable Objects/InputReader", fileName = "InputReader", order = 0)]
	public class InputReaderSO : ScriptableObject, PlayerInputActions.IPlayerActions, IInputReader
	{
		// TODO : use normal script insted of scriptable object
		public event UnityAction<Vector2> Move = delegate { };
		public event UnityAction Crouch = delegate { };
		public event UnityAction<bool> Jump = delegate { };

		public Vector2 Direction => m_input.Player.Move.ReadValue<Vector2>();

		private PlayerInputActions m_input;

		public void EnablePlayerActions()
		{
			if (m_input == null)
			{
				m_input = new PlayerInputActions();
				m_input.Player.SetCallbacks(this);
			}
			m_input.Enable();
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			Move.Invoke(context.ReadValue<Vector2>());
		}

		public void OnPrimaryAttack(InputAction.CallbackContext context)
		{
			// no-op
		}

		public void OnCrouch(InputAction.CallbackContext context)
		{
			Crouch.Invoke();
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			Jump.Invoke(context.performed);
		}

		public void OnDash(InputAction.CallbackContext context)
		{
			// no-op
		}
	}

	public interface IInputReader
	{
		Vector2 Direction { get; }
		void EnablePlayerActions();
	}
}
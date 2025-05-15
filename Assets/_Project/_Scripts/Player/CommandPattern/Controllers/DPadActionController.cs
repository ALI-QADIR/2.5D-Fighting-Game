using Smash.Player.CommandPattern.ActionCommands;
using Smash.StructsAndEnums;
using UnityEngine.InputSystem;

namespace Smash.Player.CommandPattern.Controllers
{
	public class DPadActionController : ActionController
	{
		private float m_horizontalInput;
		private float m_verticalInput;
		private DPadDirection m_currentDPadHorizontalDirection;
		private DPadDirection m_currentDPadVerticalDirection;
		
		protected override void SetupActions()
		{
			m_currentDPadHorizontalDirection = DPadDirection.None;
			m_currentDPadVerticalDirection = DPadDirection.None;
			
			InputActions.Player.Horizontal.performed += HandleHorizontal;
			InputActions.Player.Horizontal.canceled += HandleHorizontal;
			InputActions.Player.Vertical.performed += HandleVertical;
		}
		
		protected override void RemoveActions()
		{
			InputActions.Player.Horizontal.performed -= HandleHorizontal;
			InputActions.Player.Horizontal.canceled -= HandleHorizontal;
			InputActions.Player.Vertical.performed -= HandleVertical;
		}

		private void HandleHorizontal(InputAction.CallbackContext ctx)
		{
			m_horizontalInput = ctx.ReadValue<float>();
			m_currentDPadHorizontalDirection = m_horizontalInput switch
			{
				> 0.5f => DPadDirection.Right,
				< -0.5f => DPadDirection.Left,
				_ => DPadDirection.None
			};
			CreateDpadCommand(m_currentDPadHorizontalDirection, out IGameplayActionCommand command);
			AddToSequence(command);
		}

		private void HandleVertical(InputAction.CallbackContext ctx)
		{
			m_verticalInput = ctx.ReadValue<float>();
			m_currentDPadVerticalDirection = m_verticalInput switch
			{
				> 0.5f => DPadDirection.Up,
				< -0.5f => DPadDirection.Down,
				_ => DPadDirection.None
			};
			CreateDpadCommand(m_currentDPadVerticalDirection, out IGameplayActionCommand command);
			AddToSequence(command);
		}

		private static void CreateDpadCommand(in DPadDirection direction, out IGameplayActionCommand command)
		{
			command = direction switch
			{
				DPadDirection.Up => new DPadUpActionCommand(),
				DPadDirection.Down => new DPadDownActionCommand(),
				DPadDirection.Left => new DPadLeftActionCommand(),
				DPadDirection.Right => new DPadRightActionCommand(),
				_ => new DPadNullActionCommand()
			};
		}
		
		private static void DetermineDirectionFromInput(in float input, out DPadDirection direction)
		{
			direction = input switch
			{
				> 0.5f => DPadDirection.Right,
				< -0.5f => DPadDirection.Left,
				_ => DPadDirection.None
			};
		}
	}
}

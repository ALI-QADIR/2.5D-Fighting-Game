using UnityEngine.InputSystem;

namespace Smash.Player.CommandPattern
{
	public enum DPadDirection
	{
		None,
		Up,
		Down,
		Left,
		Right
	}
	
	public class DPadActionController : ActionController
	{
		private float m_horizontalInput;
		private float m_verticalInput;
		public DPadDirection CurrentDPadHorizontalDirection { get; private set; }
		public DPadDirection CurrentDPadVerticalDirection { get; private set; }
		
		protected override void SetupActions()
		{
			CurrentDPadHorizontalDirection = DPadDirection.None;
			CurrentDPadVerticalDirection = DPadDirection.None;
			InputActions.Player.Horizontal.performed += HandleHorizontal;
			InputActions.Player.Horizontal.canceled += HandleHorizontal;
			InputActions.Player.Vertical.performed += HandleVertical;
			InputActions.Player.Vertical.canceled += HandleVertical;
		}
		
		protected override void RemoveActions()
		{
			InputActions.Player.Horizontal.performed -= HandleHorizontal;
			InputActions.Player.Horizontal.canceled -= HandleHorizontal;
			InputActions.Player.Vertical.performed -= HandleVertical;
			InputActions.Player.Vertical.canceled -= HandleVertical;
		}

		private void HandleHorizontal(InputAction.CallbackContext ctx)
		{
			m_horizontalInput = ctx.ReadValue<float>();
		}

		private void HandleVertical(InputAction.CallbackContext ctx)
		{
			m_verticalInput = ctx.ReadValue<float>();
			CurrentDPadVerticalDirection = m_verticalInput switch
			{
				> 0.5f => DPadDirection.Up,
				< -0.5f => DPadDirection.Down,
				_ => DPadDirection.None
			};
			CreateDpadCommand(CurrentDPadVerticalDirection, out IGameplayActionCommand command);
			AddToSequence(command);
			ExecuteActionCommand(command);
		}

		private void Update()
		{
			HandleDPad();
		}

		private void HandleDPad()
		{
			DetermineDirectionFromInput(m_horizontalInput, out var newHorizontalDirection);
			ProcessDirectionChange(newHorizontalDirection);
		}
		
		private void ProcessDirectionChange(DPadDirection newHorizontalDirection)
		{
			if (newHorizontalDirection != CurrentDPadHorizontalDirection)
			{
				CurrentDPadHorizontalDirection = newHorizontalDirection;
				EnableCurrentDpadDirection(CurrentDPadHorizontalDirection);
			}
		}

		private void EnableCurrentDpadDirection(in DPadDirection direction)
		{
			CreateDpadCommand(direction, out IGameplayActionCommand command);
			AddToSequence(command);
			ExecuteActionCommand(command);
		}

		protected override void AddToSequence(IGameplayActionCommand command)
		{
			ComboActionQueueManager.AddCommandToComboSequence(command);
		}

		protected override void ExecuteActionCommand(IGameplayActionCommand command)
		{
			Invoker.ExecuteCommand(command);
		}

		private static void CreateDpadCommand(in DPadDirection direction, out IGameplayActionCommand command)
		{
			command = direction switch
			{
				DPadDirection.Up => new DPadUpActionCommand(),
				DPadDirection.Down => new DPadDownActionCommand(),
				DPadDirection.Left => new DPadLeftActionCommand(),
				DPadDirection.Right => new DPadRightActionCommand(),
				_ => null
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

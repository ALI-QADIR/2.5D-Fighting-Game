using JetBrains.Annotations;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.Player.CommandPattern.Controllers;
using Smash.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Player
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(PlayerInputActionsController))]
	public class PlayerController : BaseController
	{
		[SerializeField] protected PlayerInputActionsController _inputActionsController;
		[SerializeField] protected PlayerInput _playerInputComponent;
		[field: SerializeField] private ButtonActionController ButtonActionControllerComponent { get; set; }
		[field: SerializeField] private DPadActionController DPadActionControllerComponent { get; set; }


		[Header("Debug")] 
		[SerializeField] private bool m_enableInvokerDebug = true;
		[SerializeField] private bool m_enableQueueDebug = true;
		
		protected PlayerInputActions _inputActions;
		protected UiPawn _possessedUiPawn;
		
		public int PlayerIndex { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			_inputActionsController ??= GetComponent<PlayerInputActionsController>();
			_playerInputComponent ??= GetComponent<PlayerInput>();
			ButtonActionControllerComponent ??= GetComponent<ButtonActionController>();
			DPadActionControllerComponent ??= GetComponent<DPadActionController>();
			
			_playerInputComponent.onDeviceLost += DeviceLost;
			_playerInputComponent.onDeviceRegained += DeviceRegained;
			
			_playerInputComponent.onDeviceLost += DeviceLost;
			_playerInputComponent.onDeviceRegained += DeviceRegained;
			
			CommandInvoker.Debugging = m_enableInvokerDebug;
			ComboQueueManager.Debugging = m_enableQueueDebug;
		}

		private void OnDestroy()
		{
			_playerInputComponent.onDeviceLost -= DeviceLost;
			_playerInputComponent.onDeviceRegained -= DeviceRegained;
		}

		public void Initialise([NotNull] UiPawn uiPawn)
		{
			_possessedUiPawn = uiPawn;
			_possessedUiPawn.Initialise();
			
			Initialise();
		}

		public void SetPawn(CharacterPawn pawn)
		{
			_possessedCharacterPawn = pawn;
			_possessedCharacterPawn.Initialise();
		}

		public void SetPawn(UiPawn pawn)
		{
			_possessedUiPawn = pawn;
			_possessedUiPawn.Initialise();
		}

		public void EnablePlayerInputAndDisableUiInput()
		{
			_inputActionsController.SetUiInputEnabled(false);
			_inputActionsController.SetPlayerInputEnabled(true);
		}

		public void EnableUiInputAndDisablePlayerInput()
		{
			_inputActionsController.SetPlayerInputEnabled(false);
			_inputActionsController.SetUiInputEnabled(true);
		}

		private void Initialise()
		{
			_inputActions = _inputActionsController.InitialiseInputActions();
			PlayerIndex = _playerInputComponent.playerIndex;
			InitialiseActionControllers();
		}

		private void InitialiseActionControllers()
		{
			ButtonActionControllerComponent.Initialise(_inputActions, ComboQueueManager);
			DPadActionControllerComponent.Initialise(_inputActions, ComboQueueManager);
		}

		protected override void OnCommandExecutionStarted(IGameplayActionCommand command)
		{
			command.StartActionExecution(_inputHandler);
		}

		protected override void OnCommandExecutionFinished(IGameplayActionCommand command)
		{
			command.FinishActionExecution(_inputHandler);
		}
		
		private void DeviceLost(PlayerInput _)
		{
			Debug.Log("Device lost");
		}

		private void DeviceRegained(PlayerInput _)
		{
			Debug.Log("Device regained");
		}
	}
}

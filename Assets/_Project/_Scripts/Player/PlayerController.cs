using JetBrains.Annotations;
using Smash.Player.CommandPattern;
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
		[field: SerializeField] private UiActionController UiActionControllerComponent { get; set; }


		[Header("Debug")] 
		[SerializeField] private bool m_enableInvokerDebug = true;
		[SerializeField] private bool m_enableQueueDebug = true;
		
		protected PlayerInputActions _inputActions;
		protected UiPawn _possessedUiPawn;
		protected UiActionCommandInvoker UiCommandInvoker { get; private set; }
		
		public int PlayerIndex { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			_inputActionsController ??= GetComponent<PlayerInputActionsController>();
			_playerInputComponent ??= GetComponent<PlayerInput>();
			ButtonActionControllerComponent ??= GetComponent<ButtonActionController>();
			DPadActionControllerComponent ??= GetComponent<DPadActionController>();
			UiActionControllerComponent ??= GetComponent<UiActionController>();
			
			_playerInputComponent.onDeviceLost += DeviceLost;
			_playerInputComponent.onDeviceRegained += DeviceRegained;
			
			_playerInputComponent.onDeviceLost += DeviceLost;
			_playerInputComponent.onDeviceRegained += DeviceRegained;
			
			GameplayCommandInvoker.Debugging = m_enableInvokerDebug;
			ComboQueueManager.Debugging = m_enableQueueDebug;
		}

		protected override void InitialiseCommandInvoker()
		{
			base.InitialiseCommandInvoker();
			UiCommandInvoker ??= new UiActionCommandInvoker();
			UiCommandInvoker.OnCommandExecutionFinished += UiCommandExecutionFinished;
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

		public void DisableUiInput()
		{
			_inputActionsController.SetUiInputEnabled(false);
		}

		public void DisablePlayerInput()
		{
			_inputActionsController.SetPlayerInputEnabled(false);
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
			UiActionControllerComponent.Initialise(_inputActions, ComboQueueManager);
		}

		protected override void OnGameplayCommandExecutionStarted(IGameplayActionCommand command)
		{
			command.StartActionExecution(_inputHandler);
		}

		protected override void OnGameplayCommandExecutionFinished(IGameplayActionCommand command)
		{
			command.FinishActionExecution(_inputHandler);
		}

		private void UiCommandExecutionFinished(IGameplayActionCommand command)
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

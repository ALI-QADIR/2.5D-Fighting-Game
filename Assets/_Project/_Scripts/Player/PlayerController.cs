using Smash.Player.CommandPattern;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.Player.CommandPattern.Controllers;
using Smash.Player.Input;
using Smash.System;
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
		protected UiActionCommandInvoker UiCommandInvoker { get; private set; }
		
		[field:SerializeField] public int PlayerIndex { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			transform.SetParent(PlayerControllerManager.Instance.transform);
			_inputActionsController ??= GetComponent<PlayerInputActionsController>();
			_playerInputComponent ??= GetComponent<PlayerInput>();
			ButtonActionControllerComponent ??= GetComponent<ButtonActionController>();
			DPadActionControllerComponent ??= GetComponent<DPadActionController>();
			UiActionControllerComponent ??= GetComponent<UiActionController>();
			
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

		private void OnDisable()
		{
			_playerInputComponent.onDeviceLost -= DeviceLost;
			_playerInputComponent.onDeviceRegained -= DeviceRegained;
			_inputActionsController.SetUiInputEnabled(false);
			_inputActionsController.SetPlayerInputEnabled(false);
		}

		public void Initialise()
		{
			_inputActions = _inputActionsController.InitialiseInputActions();
			PlayerIndex = _playerInputComponent.playerIndex;
			InitialiseActionControllers();
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
		
		private void InitialiseActionControllers()
		{
			ButtonActionControllerComponent.Initialise(_inputActions, ComboQueueManager);
			DPadActionControllerComponent.Initialise(_inputActions, ComboQueueManager);
			UiActionControllerComponent.Initialise(_inputActions, ComboQueueManager);
		}

		protected override void OnGameplayCommandExecutionStarted(IGameplayActionCommand command)
		{
			Debug.Log("Command started, player index: " + PlayerIndex, gameObject);
			command.SetCommandParameters(PlayerIndex);
			SetEventArgs(command);
			InvokeEvent();
		}

		protected override void OnGameplayCommandExecutionFinished(IGameplayActionCommand command)
		{
			Debug.Log("Command finished, player index: " + PlayerIndex, gameObject);
			command.SetCommandParameters(PlayerIndex, true);
			SetEventArgs(command);
			InvokeEvent();
		}

		private void UiCommandExecutionFinished(IGameplayActionCommand command)
		{
			Debug.Log("Command started, player index: " + PlayerIndex, gameObject);
			command.SetCommandParameters(PlayerIndex, true);
			SetEventArgs(command);
			InvokeEvent();
		}
		
		private void DeviceLost(PlayerInput _)
		{
			PlayerControllerManager.Instance.PlayerDeviceLost(PlayerIndex);
			Debug.Log("Device lost");
		}

		private void DeviceRegained(PlayerInput _)
		{
			PlayerControllerManager.Instance.PlayerDeviceRegained(PlayerIndex);
			Debug.Log("Device regained");
		}
	}
}

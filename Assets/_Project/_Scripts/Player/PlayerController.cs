using Smash.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Player
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(PlayerInputActionsController))]
	public abstract class PlayerController : BaseController
	{
		[SerializeField] protected PlayerInputActionsController _inputActionsController;
		[SerializeField] protected PlayerInput _playerInputComponent;
		
		protected PlayerInputActions _inputActions;
		
		public int PlayerIndex { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			_inputActionsController ??= GetComponent<PlayerInputActionsController>();
			_playerInputComponent ??= GetComponent<PlayerInput>();
			
			_playerInputComponent.onDeviceLost += DeviceLost;
			_playerInputComponent.onDeviceRegained += DeviceRegained;
		}

		protected virtual void Initialise()
		{
			_inputActions = _inputActionsController.InitialiseInputActions();
			
			PlayerIndex = _playerInputComponent.playerIndex;
		}

		private void OnDestroy()
		{
			_playerInputComponent.onDeviceLost -= DeviceLost;
			_playerInputComponent.onDeviceRegained -= DeviceRegained;
		}

		private void DeviceLost(PlayerInput _)
		{
			Debug.Log("Device lost");
		}

		private void DeviceRegained(PlayerInput _)
		{
			Debug.Log("Device regained");
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
	}
}

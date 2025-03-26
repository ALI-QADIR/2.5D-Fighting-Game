using Smash.Player.CommandPattern.ActionCommands;
using Smash.Player.CommandPattern.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Player.Input
{
	[RequireComponent(typeof(ButtonActionController))]
	[RequireComponent(typeof(DPadActionController))]
	[RequireComponent(typeof(PlayerInputActionsController))]
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerController : BaseController
	{
		[field: SerializeField] private ButtonActionController ButtonActionControllerComponent { get; set; }
		[field: SerializeField] private DPadActionController DPadActionControllerComponent { get; set; }
		[SerializeField] private PlayerInputActionsController m_inputActionsController;
		[SerializeField] private PlayerInput m_playerInputComponent;

		[Header("Controller Properties")]
		[field: SerializeField]
		public int PlayerIndex { get; private set; }

		private PlayerInputActions m_inputActions;

		[Header("Debug")] 
		[SerializeField] private bool m_enableInvokerDebug = true;
		[SerializeField] private bool m_enableQueueDebug = true;

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
			ButtonActionControllerComponent ??= GetComponent<ButtonActionController>();
			DPadActionControllerComponent ??= GetComponent<DPadActionController>();
			m_playerInputComponent.onDeviceLost += DeviceLost;
			m_playerInputComponent.onDeviceRegained += DeviceRegained;
			
			CommandInvoker.Debugging = m_enableInvokerDebug;
			ComboQueueManager.Debugging = m_enableQueueDebug;
		}

		private void OnDestroy()
		{
			m_playerInputComponent.onDeviceLost -= DeviceLost;
			m_playerInputComponent.onDeviceRegained -= DeviceRegained;
		}

		#endregion Unity Methods

		#region Public Methods

		public override void Initialise(PlayerPawn pawn)
		{
			base.Initialise(pawn);
			Initialise();
		}

		public void Initialise()
		{
			m_inputActionsController ??= GetComponent<PlayerInputActionsController>();
			m_inputActions = m_inputActionsController.InitialiseInputActions();
			
			m_playerInputComponent ??= GetComponent<PlayerInput>();
			PlayerIndex = m_playerInputComponent.playerIndex;
			
			InitialiseActionControllers();
		}
		
		public void EnablePlayerInputAndDisableUiInput()
		{
			m_inputActionsController.SetUiInputEnabled(false);
			m_inputActionsController.SetPlayerInputEnabled(true);
		}
		
		public void EnableUiInputAndDisablePlayerInput()
		{
			m_inputActionsController.SetPlayerInputEnabled(false);
			m_inputActionsController.SetUiInputEnabled(true);
		}

		#endregion  Public Methods

		#region Private Methods

		private void InitialiseActionControllers()
		{
			ButtonActionControllerComponent.Initialise(m_inputActions, ComboQueueManager);
			DPadActionControllerComponent.Initialise(m_inputActions, ComboQueueManager);
		}

		protected override void OnCommandExecutionStarted(IGameplayActionCommand command)
		{
			command.StartActionExecution(_possessedPawn);
		}

		protected override void OnCommandExecutionFinished(IGameplayActionCommand command)
		{
			command.FinishActionExecution(_possessedPawn);
		}

		private void DeviceLost(PlayerInput _)
		{
			Debug.Log("Device lost");
		}

		private void DeviceRegained(PlayerInput _)
		{
			Debug.Log("Device regained");
		}

		#endregion Private Methods
	}
}

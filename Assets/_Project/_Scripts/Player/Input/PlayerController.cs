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
		[field: SerializeField] protected ButtonActionController ButtonActionController { get; private set; }
		[field: SerializeField] protected DPadActionController DPadActionController { get; private set; }
		[SerializeField] private PlayerInputActionsController m_inputActionsController;
		[SerializeField] private PlayerInput m_playerInput;

		[Header("Controller Properties")]
		[field: SerializeField] public int PlayerIndex { get; private set; }
		private PlayerInputActions m_inputActions;
		
		[Header("Debug")] 
		[SerializeField] private bool m_enableInvokerDebug = true;
		[SerializeField] private bool m_enableQueueDebug = true;

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
			ButtonActionController ??= GetComponent<ButtonActionController>();
			DPadActionController ??= GetComponent<DPadActionController>();
			m_playerInput.onDeviceLost += DeviceLost;
			m_playerInput.onDeviceRegained += DeviceRegained;
			
			CommandInvoker.Debugging = m_enableInvokerDebug;
			ComboActionQueueManager.Debugging = m_enableQueueDebug;
		}

		private void OnDestroy()
		{
			m_playerInput.onDeviceLost -= DeviceLost;
			m_playerInput.onDeviceRegained -= DeviceRegained;
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
			
			m_playerInput ??= GetComponent<PlayerInput>();
			PlayerIndex = m_playerInput.playerIndex;
			
			InitialiseCommandInvoker();
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
		
		private void InitialiseActionControllers()
		{
			ButtonActionController.Initialise(m_inputActions, CommandInvoker, ComboActionQueueManager);
			DPadActionController.Initialise(m_inputActions, CommandInvoker, ComboActionQueueManager);
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

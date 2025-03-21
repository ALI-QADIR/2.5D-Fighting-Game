using Smash.Player.CommandPattern;
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
		public int PlayerIndex { get; private set; }
		private PlayerInputActions m_inputActions;

		protected override void Awake()
		{
			base.Awake();
			ButtonActionController ??= GetComponent<ButtonActionController>();
			DPadActionController ??= GetComponent<DPadActionController>();
			m_playerInput.onDeviceLost += DeviceLost;
			m_playerInput.onDeviceRegained += DeviceRegained;
		}

		private void OnDestroy()
		{
			m_playerInput.onDeviceLost -= DeviceLost;
			m_playerInput.onDeviceRegained -= DeviceRegained;
		}

		public override void Initialise(PlayerPawn pawn)
		{
			base.Initialise(pawn);
			Initialise();
		}

		private void Initialise()
		{
			m_inputActionsController ??= GetComponent<PlayerInputActionsController>();
			m_inputActions = m_inputActionsController.InitialiseInputActions();
			
			m_playerInput ??= GetComponent<PlayerInput>();
			PlayerIndex = m_playerInput.playerIndex;
			
			InitialiseCommandInvoker();
			InitialiseActionControllers();
		}
		
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

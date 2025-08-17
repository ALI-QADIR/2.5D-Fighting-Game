using System;
using System.Collections.Generic;
using Smash.Player;
using TripleA.Utils.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.System
{
	[RequireComponent(typeof(PlayerInputManager))]
	public class PlayerControllerManager : PersistentSingleton<PlayerControllerManager>
	{
		[SerializeField] private PlayerInputManager m_playerInputManager;
		[SerializeField] private PlayerController m_controllerPrefab;
		[SerializeField] private BasePawn m_pawnPrefab;
		private Dictionary<int, PlayerController> m_controllers;

		public int CurrentPlayerCount => m_playerInputManager.playerCount;
		
		public event Action<int> OnPlayerJoined; 
		public event Action<int> OnPlayerRegained; 
		public event Action<int> OnPlayerLeft;

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
			m_controllers = new Dictionary<int, PlayerController>();
			m_playerInputManager ??= GetComponent<PlayerInputManager>();

			m_playerInputManager.playerPrefab = m_controllerPrefab.gameObject;
		}

		private void Start()
		{
			m_playerInputManager.onPlayerJoined += PlayerJoined;
			m_playerInputManager.onPlayerLeft += PlayerLeft;
		}

		private void OnDisable()
		{
			m_playerInputManager.onPlayerJoined -= PlayerJoined;
			m_playerInputManager.onPlayerLeft -= PlayerLeft;
		}
		
		private void OnValidate()
		{
			if (gameObject.activeInHierarchy)
				m_playerInputManager = GetComponent<PlayerInputManager>();
		}

		#endregion Unity Methods

		#region Public Methods
		
		public void AddInputDeviceAndJoinPlayer(int index, InputDevice device)
		{
			string controlScheme = device is Keyboard ? "Keyboard" : "Gamepad";
			m_playerInputManager.JoinPlayer(index, -1, controlScheme, device);
			OnPlayerJoined?.Invoke(index);
		}

		public void DisableAllUiInput()
		{
			foreach (var ctr in m_controllers.Values)
			{
				ctr.DisableAllInput();
			}
		}

		public void DisableAllPlayerInput()
		{
			foreach (var ctr in m_controllers.Values)
			{
				ctr.DisableAllInput();
			}
		}

		public void DisableControllerInputWithPlayerIndex(int playerIndex)
		{
			m_controllers[playerIndex].DisableAllInput();
		}

		public void EnableControllerPlayerInputWithPlayerIndex(int playerIndex)
		{
			m_controllers[playerIndex].EnablePlayerInput();
		}

		public void EnableControllerUiInputWithPlayerIndex(int playerIndex)
		{
			m_controllers[playerIndex].EnableUiInput();
		}
		
		public void PlayerDeviceRegained(int index)
		{
			OnPlayerRegained?.Invoke(index);
		}
		
		public void PlayerDeviceLost(int index)
		{
			OnPlayerLeft?.Invoke(index);
		}

		#endregion Public Methods
		
		#region Private Methods

		private void PlayerJoined(PlayerInput input)
		{
			PlayerController ctr = input.GetComponent<PlayerController>();
			int playerIndex = input.playerIndex;
			if (!m_controllers.ContainsValue(ctr))
			{
				m_controllers.Add(playerIndex, ctr);
				// var pawn = Instantiate(m_pawnPrefab, Vector3.zero, Quaternion.identity);
				// ctr.Initialise();
				// ctr.EnablePlayerInputAndDisableUiInput();
				// pawn.SetIndex(playerIndex);
				// pawn.Initialise();
			}
		}
		
		private void PlayerLeft(PlayerInput input)
		{
			Debug.Log("Player Left");
			int playerIndex = input.playerIndex;
			m_controllers.Remove(playerIndex);
		}

		#endregion Private Methods
	}
}

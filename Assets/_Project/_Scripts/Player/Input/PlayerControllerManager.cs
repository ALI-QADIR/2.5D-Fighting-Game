using System.Collections.Generic;
using TripleA.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Player.Input
{
	[RequireComponent(typeof(PlayerInputManager))]
	public class PlayerControllerManager : PersistentSingleton<PlayerControllerManager>
	{
		[SerializeField] private PlayerInputManager m_playerInputManager;
		[SerializeField] private PlayerPawn m_pawnPrefab;
		[SerializeField] private PlayerController m_controllerPrefab;
		private Dictionary<int, BaseController> m_controllers;

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
			m_controllers = new Dictionary<int, BaseController>();
			m_playerInputManager ??= GetComponent<PlayerInputManager>();

			m_playerInputManager.playerPrefab = m_controllerPrefab.gameObject;
		}

		private void Start()
		{
			m_playerInputManager.onPlayerJoined += OnPlayerJoined;
			m_playerInputManager.onPlayerLeft += OnPlayerLeft;
		}

		private void OnDisable()
		{
			m_playerInputManager.onPlayerJoined -= OnPlayerJoined;
			m_playerInputManager.onPlayerLeft -= OnPlayerLeft;
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
			m_playerInputManager.JoinPlayer(index, -1, null, device);
		}
		
		public void RemoveInputDeviceAndPlayerControllerWithIndex(int index)
		{
			m_controllers[index].Dispose();
			m_controllers.Remove(index);
		}

		#endregion Public Methods
		
		#region Private Methods

		private void OnPlayerJoined(PlayerInput input)
		{
			BaseController ctr = input.GetComponent<BaseController>();
			int playerIndex = input.playerIndex;
			if (!m_controllers.ContainsValue(ctr))
			{
				m_controllers.Add(playerIndex, ctr);
				// Todo : Insatntiation from game manager
				var pawn = Instantiate(m_pawnPrefab, Vector3.zero, Quaternion.identity);
				ctr.Initialise(pawn);
			}
		}
		
		private void OnPlayerLeft(PlayerInput input)
		{
			Debug.Log("Player Left");
			int playerIndex = input.playerIndex;
			m_controllers.Remove(playerIndex);
		}

		#endregion Private Methods
	}
}

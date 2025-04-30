using System.Collections.Generic;
using System.Linq;
using TripleA.Utils.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.System
{
	public class PlayerDevicesManager : PersistentSingleton<PlayerDevicesManager>
	{
		private InputAction m_joinInputAction;
		private List<InputDevice> m_activeInputDevices;

		private int m_currentAllowedPlayerCount;
		private int CurrentAllowedPlayerCount
		{
			get => m_currentAllowedPlayerCount;
			set =>
				m_currentAllowedPlayerCount = PlayerInputManager.instance.maxPlayerCount >= value
					? value
					: PlayerInputManager.instance.maxPlayerCount;
		}

		protected override void Awake()
		{
			base.Awake();
			m_activeInputDevices = new List<InputDevice>();

			m_joinInputAction = new InputAction(binding: "/*/*");
		}

		private void Start()
		{
			m_joinInputAction.started += JoinPerformed;
			EnablePlayerJoining(2); // TODO
		}

		private void OnDisable()
		{
			m_joinInputAction.started -= JoinPerformed;
		}

		private void OnDestroy()
		{
			m_joinInputAction.Dispose();
		}

		public void EnablePlayerJoining(int newCurrentAllowedPlayerCount)
		{
			if (CurrentAllowedPlayerCount != newCurrentAllowedPlayerCount)
			{
				CurrentAllowedPlayerCount = newCurrentAllowedPlayerCount;
				RefreshDevices();
			}
			m_joinInputAction.Enable();
		}

		public void DisablePlayerJoining()
		{
			m_joinInputAction.Disable();
		}

		private void JoinPerformed(InputAction.CallbackContext context)
		{
			// Debug.Log("Joining player");
			CheckAndAddInputDevices(context.control.device);
		}

		private void RefreshDevices()
		{
			if (m_activeInputDevices.Count > CurrentAllowedPlayerCount)
			{
				for (int i = m_activeInputDevices.Count - 1; i >= CurrentAllowedPlayerCount; i--)
				{
					PlayerControllerManager.Instance.RemoveInputDeviceAndPlayerControllerWithIndex(i);
					m_activeInputDevices.RemoveAt(i);
				}
			}
		}

		private void CheckAndAddInputDevices(InputDevice device)
		{
			if (PlayerInput.all.SelectMany(input => input.devices).Contains(device))
			{
				// Debug.Log("Device already in use");
				return;
			}

			// ignore mouse input (sanity check)
			if (device is Mouse)
			{
				// Debug.Log("Ignoring mouse input");
				return;
			}

			if (m_activeInputDevices.Count >= CurrentAllowedPlayerCount)
			{
				// Debug.Log("Max player count reached");
				return;
			}

			if (m_activeInputDevices.Contains(device))
			{
				// Debug.Log("Device already added");
				return;
			}

			m_activeInputDevices.Add(device);
			int playerIndex = m_activeInputDevices.Count - 1;
			PlayerControllerManager.Instance.AddInputDeviceAndJoinPlayer(playerIndex, device);
			Debug.Log($"Joined player {playerIndex}, device: {device.displayName}");
		}
	}
}

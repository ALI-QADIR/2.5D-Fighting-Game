using Smash.Player;
using Smash.System;
using UnityEngine;

namespace Smash.World.Test
{
	public class TestManager : MonoBehaviour
	{
		[SerializeField] private int m_playerCount = 2;
		[SerializeField] private CharacterPawn m_playerPrefab;
		
		private void Start()
		{
			PlayerDevicesManager.Instance.EnablePlayerJoining(m_playerCount);
			PlayerDevicesManager.Instance.OnPlayerJoined += PlayerJoined;
		}
		
		private void OnDisable()
		{
			if(PlayerDevicesManager.HasInstance)
				PlayerDevicesManager.Instance.OnPlayerJoined -= PlayerJoined;
		}

		private void PlayerJoined(int index)
		{
			var player = Instantiate(m_playerPrefab);
			var ctr = PlayerControllerManager.Instance.AssignPawnToController(player, index);
			ctr.EnablePlayerInputAndDisableUiInput();
		}
	}
}

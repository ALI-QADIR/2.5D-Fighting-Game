using Smash.Player;
using Smash.System;
using UnityEngine;

namespace Smash.World.Test
{
	public class TestManager : MonoBehaviour
	{
		[SerializeField] private int m_playerCount = 2;
		[SerializeField] private CharacterPawn m_characterPawnPrefab;
		[SerializeField] private UiPawn m_uiPawnPrefab;
		
		private void Start()
		{
			PlayerDevicesManager.Instance.EnablePlayerJoining(m_playerCount);
			PlayerControllerManager.Instance.OnPlayerJoined += PlayerJoined;
		}
		
		private void OnDisable()
		{
			if(PlayerControllerManager.HasInstance)
				PlayerControllerManager.Instance.OnPlayerJoined -= PlayerJoined;
		}

		private void PlayerJoined(int index)
		{
			var characterPawn = Instantiate(m_characterPawnPrefab);
			var uiPawn = Instantiate(m_uiPawnPrefab);
			var ctr = PlayerControllerManager.Instance.InitialisePawn(uiPawn, index);
			ctr.SetPawn(characterPawn);
			ctr.EnablePlayerInputAndDisableUiInput();
		}
	}
}

using Smash.Player;
using UnityEngine;

namespace Smash.System
{
	public class MainMenuManager : GameManager<MainMenuManager>
	{
		[SerializeField] private UiPawn m_playerPawnPrefab;
		
		#region Private Methods
		
		protected override void PlayerJoined(int index)
		{
			Debug.Log("Player Joined");
			var uiPawn = Instantiate(m_playerPawnPrefab);
			// Debug.Log(index);
			var ctr = PlayerControllerManager.Instance.InitialisePawn(index, uiPawn);
			PlayerControllerManager.Instance.DisableAllPlayerInput();
			PlayerControllerManager.Instance.DisableAllUiInput();
			// ctr.EnableUiInputAndDisablePlayerInput();
		}
		
		protected override void SceneLoaded()
		{
			// Debug.Log("Scene Loaded");
			PlayerDevicesManager.Instance.EnablePlayerJoining(1);
		}

		#endregion Private Methods
	}
}

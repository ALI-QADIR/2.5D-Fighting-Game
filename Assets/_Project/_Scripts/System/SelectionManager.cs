using Smash.Player;
using UnityEngine;

namespace Smash.System
{
	public class SelectionManager : GameManager<SelectionManager>
	{
		[SerializeField] private UiPawn m_playerPawnPrefab;
		
		#region Private Methods
		
		protected override void PlayerJoined(int index)
		{
			Debug.Log("Player Joined");
			var uiPawn = Instantiate(m_playerPawnPrefab);
			// Debug.Log(index);
			var ctr = PlayerControllerManager.Instance.InitialisePawn(index);
			ctr.SetPawn(uiPawn);
			PlayerControllerManager.Instance.SetAsPrimaryUiController(ctr.PlayerIndex);
			PlayerControllerManager.Instance.DisableAllPlayerInput();
			PlayerControllerManager.Instance.DisableAllUiInput();
			PlayerControllerManager.Instance.EnablePrimaryUiController();
			// ctr.EnableUiInputAndDisablePlayerInput();
		}
		
		protected override void SceneLoaded()
		{
			// Debug.Log("Scene Loaded");
			PlayerDevicesManager.Instance.EnablePlayerJoining(6);
		}

		#endregion Private Methods
	}
}

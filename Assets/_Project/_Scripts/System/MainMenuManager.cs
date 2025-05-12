using Smash.Player;
using TripleA.Utils.Singletons;
using UnityEngine;

namespace Smash.System
{
	public class MainMenuManager : GenericSingleton<MainMenuManager>
	{
		[SerializeField] private UiPawn m_playerPawnPrefab;

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
			if(AsyncSceneLoader.HasInstance)
			{
				// Debug.Log("Has Instance");
				AsyncSceneLoader.Instance.OnSceneLoadComplete += SceneLoaded;
			}

			if (PlayerControllerManager.HasInstance)
			{
				// Debug.Log("Has Device Manager Instance");
				PlayerControllerManager.Instance.OnPlayerJoined += PlayerJoined;
			}
		}

		private void OnDisable()
		{
			if(AsyncSceneLoader.HasInstance)
				AsyncSceneLoader.Instance.OnSceneLoadComplete -= SceneLoaded;
			
			if (PlayerControllerManager.HasInstance)
				PlayerControllerManager.Instance.OnPlayerJoined -= PlayerJoined;
		}

		#endregion Unity Methods

		#region Private Methods
		
		private void PlayerJoined(int index)
		{
			Debug.Log("Player Joined");
			var uiPawn = Instantiate(m_playerPawnPrefab);
			// Debug.Log(index);
			var ctr = PlayerControllerManager.Instance.InitialisePawn(uiPawn, index);
			
			PlayerControllerManager.Instance.SetAsPrimaryUiController(ctr.PlayerIndex);
			PlayerControllerManager.Instance.DisableAllPlayerInput();
			PlayerControllerManager.Instance.DisableAllUiInput();
			PlayerControllerManager.Instance.EnablePrimaryUiController();
			// ctr.EnableUiInputAndDisablePlayerInput();
		}
		
		private void SceneLoaded()
		{
			// Debug.Log("Scene Loaded");
			PlayerDevicesManager.Instance.EnablePlayerJoining(1);
		}

		#endregion Private Methods
	}
}

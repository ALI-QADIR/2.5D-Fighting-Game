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
				AsyncSceneLoader.Instance.OnSceneGroupLoaded += SceneGroupLoaded;

			if (PlayerDevicesManager.HasInstance)
				PlayerDevicesManager.Instance.OnPlayerJoined += PlayerJoined;
		}

		private void OnDisable()
		{
			if(AsyncSceneLoader.HasInstance)
				AsyncSceneLoader.Instance.OnSceneGroupLoaded -= SceneGroupLoaded;
			
			if (PlayerDevicesManager.HasInstance)
				PlayerDevicesManager.Instance.OnPlayerJoined -= PlayerJoined;
		}

		#endregion Unity Methods

		#region Private Methods
		
		private void PlayerJoined(int index)
		{
			var player = Instantiate(m_playerPawnPrefab);
			var ctr = PlayerControllerManager.Instance.AssignPawnToController(player, index);
			ctr.EnableUiInputAndDisablePlayerInput();
		}
		
		private void SceneGroupLoaded()
		{
			PlayerDevicesManager.Instance.EnablePlayerJoining(1);
		}

		#endregion Private Methods
	}
}

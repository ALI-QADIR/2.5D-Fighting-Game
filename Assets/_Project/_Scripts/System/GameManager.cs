using TripleA.Utils.Singletons;
using UnityEngine;

namespace Smash.System
{
	public abstract class GameManager<T> : GenericSingleton<T> where T : MonoBehaviour
	{
		// [SerializeField] private UiPawn m_playerPawnPrefab;

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

		protected virtual void OnDisable()
		{
			if(AsyncSceneLoader.HasInstance)
				AsyncSceneLoader.Instance.OnSceneLoadComplete -= SceneLoaded;
			
			if (PlayerControllerManager.HasInstance)
				PlayerControllerManager.Instance.OnPlayerJoined -= PlayerJoined;
		}

		#endregion Unity Methods

		#region Private Methods

		protected abstract void PlayerJoined(int index);

		protected abstract void SceneLoaded();

		#endregion Private Methods
	}
}

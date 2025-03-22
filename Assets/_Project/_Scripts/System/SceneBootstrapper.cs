using UnityEngine;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.SceneManagement;

namespace Smash.System
{
	public class SceneBootstrapper : ComponentSingleton<SceneBootstrapper>
	{
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			Debug.Log("BootStrapper Init...");
			SceneManager.LoadScene("AsyncSceneLoader", LoadSceneMode.Single);
		}
	}
}
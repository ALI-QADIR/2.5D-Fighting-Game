using System;
using Eflatun.SceneReference;

namespace Smash.StructsAndEnums
{
	[Serializable]
	public class SceneData<T> where T : Enum
	{
		public SceneReference sceneReference;
		public string SceneName => sceneReference.Name;
		public T sceneType;
	}
	
	public enum MySceneTypes
	{
		MainMenu = 0,
		CharacterSelect = 1,
		Tutorial = 2,
		TutorialUi = 3,
		Bootstrapper = 4,
	}
}

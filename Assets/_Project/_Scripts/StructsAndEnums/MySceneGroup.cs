using System;
using TripleA.SceneManagement;

namespace Smash.StructsAndEnums
{
	[Serializable]
	public class MySceneGroup : SceneGroup<MySceneTypes> { }
	
	public enum MySceneTypes
	{
		MainMenu,
		SpeedRun,
		Tutorial,
		TutorialUi,
		ActiveScene,
		Bootstrapper,
	}
}
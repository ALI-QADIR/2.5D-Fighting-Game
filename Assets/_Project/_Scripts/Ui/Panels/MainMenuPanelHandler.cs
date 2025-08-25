using System;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.StructsAndEnums;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.Panels
{
	public class MainMenuPanelHandler : PanelHandler
	{
		[SerializeField] private MainMenuHandler m_mainMenuHandler;
		
		protected override void Awake()
		{
			base.Awake();
			
			if (AsyncSceneLoader.HasInstance)
			{
				// Debug.Log("Has Instance");
				AsyncSceneLoader.Instance.OnSceneLoadComplete += SceneGroupLoaded;
			}
		}

		protected override void OnDestroy()
		{
			if (AsyncSceneLoader.HasInstance)
				AsyncSceneLoader.Instance.OnSceneLoadComplete -= SceneGroupLoaded;
		}

		private void SceneGroupLoaded()
		{
			// Debug.Log("SceneGroupLoaded");
			OpenPanel();
		}

		protected override void AuthenticateEvent(IGameplayActionCommand uiCommand)
		{
			if (_panelState != PanelState.Open) return;
			if (_eventDictionary.TryGetValue(uiCommand.GetType(), out Action action))
				action?.Invoke();
		}

		#region On-Click Methods

		public void OnClickPlayButton()
		{
			// _input.UI.Disable();
			AsyncSceneLoader.Instance.LoadSceneByType(MySceneTypes.CharacterSelect);
		}

		public void OnClickTutorialButton()
		{
			// _input.UI.Disable();
			AsyncSceneLoader.Instance.LoadSceneByIndex(1);
		}

		public override void BackButtonPressed()
		{
			Debug.Log("Quit");
			Application.Quit();
		}

		#endregion On-Click Methods
	}
}

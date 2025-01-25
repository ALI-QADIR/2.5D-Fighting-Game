using System;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Ui.Panels
{
	public class MainMenuPanelHandler : PanelHandler
	{
		[SerializeField] private MainMenuHandler m_mainMenuHandler;
		
		protected override void Awake()
		{
			base.Awake();
			OpenPanel();
			_eventDictionary.Add("btn_main_play", OnClickPlayButton);
			_eventDictionary.Add("btn_tutorial", OnClickTutorialButton);
			_eventDictionary.Add("btn_main_quit", OnClickQuitButton);
			
			_eventDictionary.Add("btn_main_leaderboard", ClosePanel);
			_eventDictionary.Add("btn_main_credits", ClosePanel);
			_eventDictionary.Add("btn_main_options", ClosePanel);
			
			_eventDictionary.Add("btn_modes_back", OpenPanel);
			_eventDictionary.Add("btn_credits_back", OpenPanel);
			_eventDictionary.Add("btn_options_back", OpenPanel);
			_eventDictionary.Add("btn_leaderboard_back", OpenPanel);
			
			_backButtonHandler.SetEventArgs("btn_main_quit", this);
		}

		protected override void AuthenticateEvent(UiEventArgs args)
		{
			if (_eventDictionary.TryGetValue(args.id.ToLower(), out Action action))
				action?.Invoke();
		}

		public override void OpenPanel()
		{
			base.OpenPanel();
			_input.UI.Cancel.Enable();
			_input.UI.Retry.Enable();
			_input.UI.Cancel.started += BackButtonPressed;
			_input.UI.Retry.started += Retry;
			
			m_mainMenuHandler.OnOpen();
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			_input.UI.Cancel.started -= BackButtonPressed;
			_input.UI.Retry.started -= Retry;
		}

		protected override void BackButtonPressed(InputAction.CallbackContext ctx)
		{
			_backButtonHandler.BackButtonPressed();
		}

		private void Retry(InputAction.CallbackContext ctx)
		{
			m_mainMenuHandler.OnRetry();
		}

		private void OnClickPlayButton()
		{
			_input.UI.Disable();
			AsyncSceneLoader.Instance.LoadSceneGroupByIndex(2);
		}

		private void OnClickTutorialButton()
		{
			_input.UI.Disable();
			AsyncSceneLoader.Instance.LoadSceneGroupByIndex(1);
		}

		private void OnClickQuitButton()
		{
			Application.Quit();
		}
	}
}

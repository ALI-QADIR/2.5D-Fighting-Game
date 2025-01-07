using System;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Ui.Panels
{
	public class MainMenuPanelHandler : PanelHandler
	{
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
			_input.UI.Cancel.started += BackButtonPressed;
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			_input.UI.Cancel.started -= BackButtonPressed;
		}

		protected override void BackButtonPressed(InputAction.CallbackContext ctx)
		{
			_backButtonHandler.BackButtonPressed();
		}

		private void OnClickPlayButton()
		{
			ClosePanel();
		}

		private void OnClickLeaderboardButton()
		{
			throw new NotImplementedException();
		}

		private void OnClickTutorialButton()
		{
			throw new NotImplementedException();
		}

		private void OnClickQuitButton()
		{
			Application.Quit();
		}
	}
}
using System;
using Smash.System;
using UnityEngine;

namespace Smash.Ui
{
	public class MainMenuPanelHandler : PanelHandler
	{
		protected override void Awake()
		{
			base.Awake();
			OpenPanel();
			_eventDictionary.Add("btn_play", OnClickPlayButton);
			_eventDictionary.Add("btn_leaderboard", OnClickLeaderboardButton);
			_eventDictionary.Add("btn_tutorial", OnClickTutorialButton);
			_eventDictionary.Add("btn_credits", OnClickCreditsButton);
			_eventDictionary.Add("btn_options", OnClickOptionsButton);
			_eventDictionary.Add("btn_quit", OnClickQuitButton);
			_eventDictionary.Add("btn_modeselect_back", OpenPanel);
		}

		protected override void AuthenticateEvent(UiEventArgs args)
		{
			if (_eventDictionary.TryGetValue(args.id.ToLower(), out Action action))
				action?.Invoke();
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

		private void OnClickCreditsButton()
		{
			throw new NotImplementedException();
		}

		private void OnClickOptionsButton()
		{
			throw new NotImplementedException();
		}

		private void OnClickQuitButton()
		{
			Application.Quit();
		}
	}
}
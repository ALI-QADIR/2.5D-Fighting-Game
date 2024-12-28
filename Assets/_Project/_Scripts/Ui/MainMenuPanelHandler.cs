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
		}

		protected override void AuthenticateEvent(UiEventArgs args)
		{
			if (String.Equals(args.id.ToLower(), "btn_play"))
				OnClickPlayButton();
			else if (String.Equals(args.id.ToLower(), "btn_leaderboard"))
				OnClickLeaderboardButton();
			else if (String.Equals(args.id.ToLower(), "btn_tutorial"))
				OnClickTutorialButton();
			else if (String.Equals(args.id.ToLower(), "btn_credits"))
				OnClickCreditsButton();
			else if (String.Equals(args.id.ToLower(), "btn_options"))
				OnClickOptionsButton();
			else if (String.Equals(args.id.ToLower(), "btn_quit"))
				OnClickQuitButton();
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
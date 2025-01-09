using System;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.Panels
{
	public class LeaderboardPanelHandler : PanelHandler
	{
		[SerializeField] private LeaderboardHandler m_leaderboardHandler;
		
		protected override void Awake()
		{
			base.Awake();
			
			m_leaderboardHandler ??= GetComponent<LeaderboardHandler>();
			
			_eventDictionary.Add("btn_leaderboard_back", OnClickBackButton);
			_eventDictionary.Add("btn_leaderboard_refresh", OnClickRefreshButton);
			_eventDictionary.Add("btn_main_leaderboard", OnClickLeaderboardButton);
			
			_backButtonHandler.SetEventArgs("btn_leaderboard_back", this);
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

		protected override void BackButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			_backButtonHandler.BackButtonPressed();
		}
		
		private void OnClickBackButton()
		{
			ClosePanel();
		}

		private void OnClickLeaderboardButton()
		{
			OpenPanel();
			m_leaderboardHandler.OnOpen();
		}
		
		private void OnClickRefreshButton()
		{
			m_leaderboardHandler.OnClickRefresh();
		}
	}
}
using System;
using Smash.System;
using Smash.Ui.Components;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Ui.Panels
{
	public class LeaderboardPanelHandler : PanelHandler
	{
		[SerializeField] private LeaderboardHandler m_leaderboardHandler;
		[SerializeField] private ScrollHandler m_scrollHandler;

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

			m_scrollHandler.axisValue = 0;
			
			_input.UI.Cancel.Enable();
			_input.UI.VerticalScroll.Enable();
			_input.UI.VerticalScroll.performed += OnVerticalScroll;
			_input.UI.VerticalScroll.canceled += OnVerticalScroll;
			_input.UI.Cancel.started += BackButtonPressed;
		}

		public override void ClosePanel()
		{
			base.ClosePanel();
			_input.UI.Cancel.started -= BackButtonPressed;
			_input.UI.VerticalScroll.performed -= OnVerticalScroll;
			_input.UI.VerticalScroll.canceled -= OnVerticalScroll;
		}

		protected override void BackButtonPressed(InputAction.CallbackContext ctx)
		{
			_backButtonHandler.BackButtonPressed();
		}

		private void OnVerticalScroll(InputAction.CallbackContext ctx)
		{
			m_scrollHandler.axisValue = ctx.ReadValue<float>();
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
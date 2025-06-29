﻿using System;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.StructsAndEnums;
using Smash.System;
using Smash.Ui.System;
using TMPro;
using UnityEngine;

namespace Smash.Ui.Panels
{
	public class SpeedRunOptionsPanel : PanelHandler
	{
		[SerializeField] private TMP_Text m_title;
		[SerializeField] private TMP_Text m_startBtnText;
		
		protected override void Awake()
		{
			base.Awake();
			
			// _eventDictionary.Add("btn_speedrun_resume", StartCountdown);
			// _eventDictionary.Add("btn_speedrun_mainmenu", OnClickMainMenuButton);
			// 
			// _backButtonHandler.SetEventArgs("btn_speedrun_mainmenu", this);
		}
		
		protected override void AuthenticateEvent(IGameplayActionCommand uiCommand)
		{
			if (_panelState != PanelState.Open) return;
			if (_eventDictionary.TryGetValue(uiCommand.GetType(), out Action action))
				action?.Invoke();
		}

		public void BeginGame()
		{
			m_title.text = "begin???";
			m_startBtnText.text = "start";
			OpenPanel();
		}

		public void EndGame(string time)
		{
			m_title.text = $"You finished in {time} seconds!!";
			m_startBtnText.text = "retry";
			OpenPanel();
		}
		
		public override void OpenPanel()
		{
			base.OpenPanel();
			// _input.UI.Cancel.Enable();
		}
		
		public override void ClosePanel()
		{
			base.ClosePanel();
			// _input.UI.Cancel.started -= BackButtonPressed;
		}

		private void StartCountdown()
		{
			ClosePanel();
			SpeedRunManager.Instance.ShouldBeginCountdown();
		}

		private void OnClickMainMenuButton()
		{
			if(!AsyncSceneLoader.HasInstance) return;
			// _input.UI.Disable();
			AsyncSceneLoader.Instance.LoadSceneByIndex(0);
		}
	}
}

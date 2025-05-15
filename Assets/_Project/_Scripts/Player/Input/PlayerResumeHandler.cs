using System;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Smash.Player.Input
{
	public class PlayerResumeHandler : UiEventListener
	{
		[FormerlySerializedAs("m_playerPlayerInput")] [FormerlySerializedAs("m_playerPlayerController")] [SerializeField] private PlayerInputActionsController m_playerPlayerInputActionsController;

		protected override void Awake()
		{
			base.Awake();
			// _eventDictionary.Add("btn_pause_resume", OnResume);
		}
		
		protected override void AuthenticateEvent(IGameplayActionCommand uiCommand)
		{
			if (_eventDictionary.TryGetValue(uiCommand.GetType(), out Action action))
				action?.Invoke();
		}
		
		private void OnResume()
		{
			// m_playerPlayerController.EnablePlayerInput();
		}
	}
}
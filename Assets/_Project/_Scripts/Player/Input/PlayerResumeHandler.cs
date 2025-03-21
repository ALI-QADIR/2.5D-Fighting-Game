using System;
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
			_eventDictionary.Add("btn_pause_resume", OnResume);
		}
		
		protected override void AuthenticateEvent(UiEventArgs args)
		{
			if (_eventDictionary.TryGetValue(args.id.ToLower(), out Action action))
				action?.Invoke();
		}
		
		private void OnResume()
		{
			// m_playerPlayerController.EnablePlayerInput();
		}
	}
}
using System;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Player.Input
{
	public class PlayerResumeHandler : UiEventListener
	{
		[SerializeField] private InputReader m_playerInputReader;

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
			m_playerInputReader.EnablePlayerInput();
		}
	}
}
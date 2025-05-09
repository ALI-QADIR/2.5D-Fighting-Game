using System;
using System.Collections.Generic;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.System;
using TripleA.EventSystem.EventChannel;

namespace Smash.Ui.System
{
	public class UiEventListener : EventListener<IGameplayActionCommand>
	{
		protected Dictionary<Type, Action> _eventDictionary;
			
		protected override void Awake()
		{
			base.Awake();
			_unityEvent.AddListener(AuthenticateEvent);
			_eventDictionary = new Dictionary<Type, Action>();
		}

		protected virtual void AuthenticateEvent(IGameplayActionCommand uiCommand)
		{
		}
	}
}

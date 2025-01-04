using System;
using System.Collections.Generic;
using Smash.System;
using TripleA.EventSystem;

namespace Smash.Ui
{
	public class UiEventListener : EventListener<UiEventArgs>
	{
		protected Dictionary<string, Action> _eventDictionary = new Dictionary<string, Action>(); 
			
		protected override void Awake()
		{
			base.Awake();
			_unityEvent.AddListener(AuthenticateEvent);
		}

		protected virtual void AuthenticateEvent(UiEventArgs args)
		{
		}
	}
}
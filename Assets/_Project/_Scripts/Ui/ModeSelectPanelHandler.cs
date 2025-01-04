using System;
using Smash.System;

namespace Smash.Ui
{
	public class ModeSelectPanelHandler : PanelHandler
	{
		protected override void Awake()
		{
			base.Awake();
			_eventDictionary.Add("btn_play", OnClickPlayButton);
			_eventDictionary.Add("btn_modeselect_back", OnClickBackButton);
		}

		protected override void AuthenticateEvent(UiEventArgs args)
		{
			if (_eventDictionary.TryGetValue(args.id.ToLower(), out Action action))
				action?.Invoke();
		}

		private void OnClickPlayButton()
		{
			OpenPanel();
		}
		
		private void OnClickBackButton()
		{
			ClosePanel();
		}
	}
}
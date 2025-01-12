using Smash.Ui.System;

namespace Smash.Player.Input
{
	public class PlayerPauseHandler : UiEventInvoker
	{
		public void OnPause()
		{
			InvokeEvent();
		}
	}
}
using Smash.Ui.System;

namespace Smash.Ui.Components
{
	public class BackButtonHandler : UiEventInvoker
	{
		public void BackButtonPressed()
		{
			InvokeEvent();
		}
	}
}
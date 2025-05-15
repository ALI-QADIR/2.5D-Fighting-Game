using System;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.StructsAndEnums;
using Smash.System;
using Smash.Ui.System;

namespace Smash.Ui.Panels
{
	public class CreditsPanelHandler : PanelHandler
	{
		protected override void AuthenticateEvent(IGameplayActionCommand uiCommand)
		{
			if (_panelState != PanelState.Open) return;
			if (_eventDictionary.TryGetValue(uiCommand.GetType(), out Action action))
				action?.Invoke();
		}

		public override void BackButtonPressed()
		{
			base.BackButtonPressed();
			ClosePanel();
		}
	}
}

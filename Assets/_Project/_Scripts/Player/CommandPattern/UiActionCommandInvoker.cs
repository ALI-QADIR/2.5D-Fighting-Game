using System;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern
{
	public class UiActionCommandInvoker : ICommandInvoker
	{
		public event Action<IGameplayActionCommand> OnCommandExecutionStarted; 
		public event Action<IGameplayActionCommand> OnCommandExecutionFinished; 
		
		public void InvokeStartCommand(IGameplayActionCommand command)
		{
			// no-op
			OnCommandExecutionStarted?.Invoke(command);
		}

		public void InvokeFinishCommand(IGameplayActionCommand command)
		{
			OnCommandExecutionFinished?.Invoke(command);
		}
	}
}

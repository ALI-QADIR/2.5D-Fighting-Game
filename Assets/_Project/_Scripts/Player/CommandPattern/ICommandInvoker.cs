using System;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern
{
	public interface ICommandInvoker
	{
		public event Action<IGameplayActionCommand> OnCommandExecutionStarted; 
		public event Action<IGameplayActionCommand> OnCommandExecutionFinished;

		public void InvokeStartCommand(IGameplayActionCommand command);

		public void InvokeFinishCommand(IGameplayActionCommand command);
	}
}

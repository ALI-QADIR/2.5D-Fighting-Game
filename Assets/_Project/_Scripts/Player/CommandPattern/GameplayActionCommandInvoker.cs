using System;
using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class GameplayActionCommandInvoker
	{
		public bool Debugging { get; set; }
		
		public event Action<IGameplayActionCommand> OnCommandExecutionStarted; 
		public event Action<IGameplayActionCommand> OnCommandExecutionFinished; 
		
		public void InvokeStartCommand(IGameplayActionCommand command)
		{
			DebugLog($"Starting Command: {command.ActionName}");
			OnCommandExecutionStarted?.Invoke(command);
		}

		public void InvokeFinishCommand(IGameplayActionCommand command)
		{
			DebugLog($"Executing Command: {command.ActionName} --- Held Duration: {command.HeldDuration}");
			OnCommandExecutionFinished?.Invoke(command);
		}
		
		private void DebugLog(string message)
		{
			if (!Debugging) return;
			Debug.LogWarning(message);
		}
	}
}

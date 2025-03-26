using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class GameplayActionCommandInvoker
	{
		public bool Debugging { get; set; }
		
		public void StartCommandExecution(IGameplayActionCommand command)
		{
			DebugLog($"Starting Command: {command.ActionName}");
			command.StartActionExecution();
		}

		public void FinishCommandExecution(IGameplayActionCommand command)
		{
			DebugLog($"Executing Command: {command.ActionName} --- Held Duration: {command.HeldDuration}");
			command.FinishActionExecution();
		}
		
		private void DebugLog(string message)
		{
			if (!Debugging) return;
			Debug.LogWarning(message);
		}
	}
}

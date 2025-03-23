using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class GameplayActionCommandInvoker
	{
		public bool Debugging { get; set; }

		public void ExecuteCommand(IGameplayActionCommand command)
		{
			DebugLog($"Executing Command: {command.ActionName} \n" +
			         $"Held Duration: {command.HeldDuration}");
			command.ExecuteAction();
		}
		
		private void DebugLog(string message)
		{
			if (!Debugging) return;
			Debug.LogWarning(message);
		}
	}
}

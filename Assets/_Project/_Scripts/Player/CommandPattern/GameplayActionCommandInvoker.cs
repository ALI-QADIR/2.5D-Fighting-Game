using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class GameplayActionCommandInvoker
	{
		public bool Debugging { get; set; }

		public void ExecuteCommand(IGameplayActionCommand command)
		{
			DebugLog($"Executing Command: {command.ActionName}");
			command.ExecuteAction();
		}
		
		private void DebugLog(string message)
		{
			if (!Debugging) return;
			Debug.Log(message);
		}
	}
}

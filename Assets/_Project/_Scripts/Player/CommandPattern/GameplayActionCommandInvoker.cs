using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class GameplayActionCommandInvoker
	{
		public void ExecuteCommand(IGameplayActionCommand command)
		{
			Debug.Log($"Executing Command: {command.ActionName}");
			command.ExecuteAction();
		}
	}
}

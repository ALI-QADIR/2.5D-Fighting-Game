using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player
{
	public class CommandEventInvoker : MonoBehaviour
	{
		[Header("Event Channel")]
		[SerializeField] private CommandEventChannel m_eventChannel;
		private IGameplayActionCommand m_actionCommand;
		
		public void SetEventArgs(IGameplayActionCommand command)
		{
			m_actionCommand = command;
		}

		protected virtual void InvokeEvent() => m_eventChannel.Invoke(m_actionCommand);
	}
}

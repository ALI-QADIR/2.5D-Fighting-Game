using Smash.Player.CommandPattern.ActionCommands;
using Smash.System;
using UnityEngine;

namespace Smash.Ui.System
{
	public abstract class UiEventInvoker : MonoBehaviour
	{
		[Header("Event Channel")]
		[SerializeField] private UiEventChannel m_eventChannel;
		private IGameplayActionCommand m_uiActionCommand;
		
		public void SetEventArgs(IGameplayActionCommand command)
		{
			m_uiActionCommand = command;
		}

		protected virtual void InvokeEvent() => m_eventChannel.Invoke(m_uiActionCommand);
	}
}
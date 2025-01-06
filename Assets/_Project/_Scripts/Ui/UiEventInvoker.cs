using Smash.System;
using UnityEngine;

namespace Smash.Ui
{
	public abstract class UiEventInvoker : MonoBehaviour
	{
		[Header("Event Channel")]
		[SerializeField] private UiEventChannel m_eventChannel;
		[SerializeField] private UiEventArgs m_eventArgs;

		protected virtual void Awake()
		{
			m_eventArgs.sender = this;
		}
		
		public void SetEventArgs(string id, MonoBehaviour sender)
		{
			m_eventArgs.sender = sender;
			m_eventArgs.id = id;
		}

		protected virtual void InvokeEvent() => m_eventChannel.Invoke(m_eventArgs);
	}
}
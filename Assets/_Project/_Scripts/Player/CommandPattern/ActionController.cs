using System;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public abstract class ActionController : MonoBehaviour
	{
		protected PlayerInputActions InputActions { get; private set; }
		protected GameplayActionCommandInvoker Invoker { get; private set; }
		protected ComboActionQueueManager ComboActionQueueManager { get; private set; }

		public virtual void Initialise(PlayerInputActions inputActions, GameplayActionCommandInvoker invoker,
			ComboActionQueueManager comboActionQueueManager)
		{
			InputActions = inputActions;
			Invoker = invoker;
			ComboActionQueueManager = comboActionQueueManager;
			SetupActions();
		}

		protected virtual void OnDestroy()
		{
			RemoveActions();
		}

		protected abstract void SetupActions();

		protected abstract void RemoveActions();
		protected abstract void ExecuteActionCommand(IGameplayActionCommand command);
		protected abstract void AddToSequence(IGameplayActionCommand command);
	}
}

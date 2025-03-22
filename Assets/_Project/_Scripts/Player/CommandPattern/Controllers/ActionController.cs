using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern.Controllers
{
	public abstract class ActionController : MonoBehaviour
	{
		protected PlayerInputActions InputActions { get; private set; }
		private GameplayActionCommandInvoker m_invoker;
		private ComboActionQueueManager m_comboActionQueueManager;

		public virtual void Initialise(PlayerInputActions inputActions, GameplayActionCommandInvoker invoker,
			ComboActionQueueManager comboActionQueueManager)
		{
			InputActions = inputActions;
			m_invoker = invoker;
			m_comboActionQueueManager = comboActionQueueManager;
			SetupActions();
		}

		protected virtual void OnDestroy()
		{
			RemoveActions();
		}

		protected abstract void SetupActions();

		protected abstract void RemoveActions();

		protected void ExecuteActionCommand(IGameplayActionCommand command)
		{
			m_invoker.ExecuteCommand(command);
		}

		protected void AddToSequence(IGameplayActionCommand command)
		{
			m_comboActionQueueManager.AddCommandToComboSequence(command);
		}
	}
}

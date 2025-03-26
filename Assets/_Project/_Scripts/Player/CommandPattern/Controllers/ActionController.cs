using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern.Controllers
{
	public abstract class ActionController : MonoBehaviour
	{
		protected PlayerInputActions InputActions { get; private set; }
		protected ComboActionQueueManager _comboActionQueueManager;

		public virtual void Initialise(PlayerInputActions inputActions, ComboActionQueueManager comboActionQueueManager)
		{
			InputActions = inputActions;
			_comboActionQueueManager = comboActionQueueManager;
			SetupActions();
		}

		protected virtual void OnDestroy()
		{
			RemoveActions();
		}

		protected abstract void SetupActions();

		protected abstract void RemoveActions();

		protected void AddToSequence(IGameplayActionCommand command)
		{
			_comboActionQueueManager.AddCommandToComboSequence(command);
		}
	}
}

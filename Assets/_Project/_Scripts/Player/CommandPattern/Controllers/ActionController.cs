using Smash.Player.CommandPattern.ActionCommands;
using Smash.Player.Input;
using UnityEngine;

namespace Smash.Player.CommandPattern.Controllers
{
	public abstract class ActionController : MonoBehaviour
	{
		protected ComboActionQueueManager _comboActionQueueManager;
		protected PlayerInputActionsController _inputActionsController;
		
		public void Initialise(PlayerInputActionsController inputActionsController, ComboActionQueueManager comboActionQueueManager)
		{
			_inputActionsController = inputActionsController;
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

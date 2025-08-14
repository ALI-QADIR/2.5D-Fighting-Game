using Smash.Player.CommandPattern.ActionCommands;
using TripleA.EventSystem.EventChannel;
using UnityEngine;

namespace Smash.Player
{
	[CreateAssetMenu(fileName = "Command Event Channel",
		menuName = "Scriptable Objects/Event Channels/Command Event Channel")]
	public class CommandEventChannel : EventChannel<IGameplayActionCommand>
	{

	}

	public abstract class CommandEventListener : EventListener<IGameplayActionCommand>
	{
		protected override void Awake()
		{
			base.Awake();
			_unityEvent.AddListener(OnCommand);
		}

		protected abstract void OnCommand(IGameplayActionCommand command);
	}

}

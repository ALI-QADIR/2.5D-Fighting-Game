using Smash.Player.CommandPattern;
using Smash.Player.CommandPattern.ActionCommands;
using TripleA.Utils.Extensions;
using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(ComboActionQueueManager))]
	[RequireComponent(typeof(InputHandler))]
	public abstract class BaseController : MonoBehaviour
	{
		[Header("Components")]
		[field: SerializeField] protected ComboActionQueueManager ComboQueueManager { get; private set; }

		protected CharacterPawn _possessedCharacterPawn;
		protected InputHandler _inputHandler;
		protected GameplayActionCommandInvoker CommandInvoker { get; private set; }

		protected virtual void Awake()
		{
			InitialiseCommandInvoker();
			_inputHandler ??= gameObject.GetOrAddComponent<InputHandler>();
			ComboQueueManager ??= GetComponent<ComboActionQueueManager>();
			ComboQueueManager.SetCommandInvoker(CommandInvoker);
		}

		public void Dispose()
		{
			CommandInvoker.OnCommandExecutionStarted -= OnCommandExecutionStarted;
			CommandInvoker.OnCommandExecutionFinished -= OnCommandExecutionFinished;
			Destroy(_possessedCharacterPawn.gameObject);
			Destroy(gameObject);
		}
		
		// public void SetPawn(CharacterPawn pawn) => _possessedPawn = pawn;

		protected void InitialiseCommandInvoker()
		{
			CommandInvoker ??= new GameplayActionCommandInvoker();
			CommandInvoker.OnCommandExecutionStarted += OnCommandExecutionStarted;
			CommandInvoker.OnCommandExecutionFinished += OnCommandExecutionFinished;
		}

		protected abstract void OnCommandExecutionStarted(IGameplayActionCommand command);
		protected abstract void OnCommandExecutionFinished(IGameplayActionCommand command);
	}
}

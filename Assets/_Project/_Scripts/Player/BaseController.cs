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
		protected GameplayActionCommandInvoker GameplayCommandInvoker { get; private set; }

		protected virtual void Awake()
		{
			InitialiseCommandInvoker();
			_inputHandler ??= gameObject.GetOrAddComponent<InputHandler>();
			ComboQueueManager ??= GetComponent<ComboActionQueueManager>();
			ComboQueueManager.SetCommandInvoker(GameplayCommandInvoker);
		}

		public virtual void Dispose()
		{
			GameplayCommandInvoker.OnCommandExecutionStarted -= OnGameplayCommandExecutionStarted;
			GameplayCommandInvoker.OnCommandExecutionFinished -= OnGameplayCommandExecutionFinished;
			
			if (_possessedCharacterPawn)
				Destroy(_possessedCharacterPawn.gameObject);
			
			Destroy(gameObject);
		}
		
		// public void SetPawn(CharacterPawn pawn) => _possessedPawn = pawn;

		protected virtual void InitialiseCommandInvoker()
		{
			GameplayCommandInvoker ??= new GameplayActionCommandInvoker();
			GameplayCommandInvoker.OnCommandExecutionStarted += OnGameplayCommandExecutionStarted;
			GameplayCommandInvoker.OnCommandExecutionFinished += OnGameplayCommandExecutionFinished;
		}

		protected abstract void OnGameplayCommandExecutionStarted(IGameplayActionCommand command);
		protected abstract void OnGameplayCommandExecutionFinished(IGameplayActionCommand command);
	}
}

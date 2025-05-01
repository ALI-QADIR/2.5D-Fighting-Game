using Smash.Player.CommandPattern;
using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(ComboActionQueueManager))]
	public abstract class BaseController : MonoBehaviour
	{
		[Header("Components")]
		[field: SerializeField] protected ComboActionQueueManager ComboQueueManager { get; private set; }

		protected BasePawn _possessedPawn;
		protected InputHandler _inputHandler;
		protected GameplayActionCommandInvoker CommandInvoker { get; private set; }

		protected virtual void Awake()
		{
			InitialiseCommandInvoker();
			ComboQueueManager ??= GetComponent<ComboActionQueueManager>();
			ComboQueueManager.SetCommandInvoker(CommandInvoker);
		}

		public void Initialise(BasePawn pawn)
		{
			_possessedPawn = pawn;
			_possessedPawn.Initialise(this);
			_inputHandler = pawn.GetInputHandler();
		}

		public virtual void Initialise(CharacterPawn pawn)
		{
			_possessedPawn = pawn;
			_possessedPawn.Initialise(this);
			_inputHandler = pawn.GetInputHandler();
		}
		
		public virtual void Initialise(UiPawn pawn)
		{
			_possessedPawn = pawn;
			_possessedPawn.Initialise(this);
			_inputHandler = pawn.GetInputHandler();
		}

		public void Dispose()
		{
			CommandInvoker.OnCommandExecutionStarted -= OnCommandExecutionStarted;
			CommandInvoker.OnCommandExecutionFinished -= OnCommandExecutionFinished;
			Destroy(_possessedPawn.gameObject);
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

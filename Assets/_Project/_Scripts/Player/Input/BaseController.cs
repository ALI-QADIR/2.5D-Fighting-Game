using Smash.Player.CommandPattern;
using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.Input
{
	[RequireComponent(typeof(ComboActionQueueManager))]
	public abstract class BaseController : MonoBehaviour
	{
		[Header("Components")]
		[field: SerializeField] protected ComboActionQueueManager ComboQueueManager { get; private set; }

		protected PlayerPawn _possessedPawn;
		protected GameplayActionCommandInvoker CommandInvoker { get; private set; }

		protected virtual void Awake()
		{
			InitialiseCommandInvoker();
			ComboQueueManager ??= GetComponent<ComboActionQueueManager>();
			ComboQueueManager.SetCommandInvoker(CommandInvoker);
		}

		public virtual void Initialise(PlayerPawn pawn)
		{
			_possessedPawn = pawn;
			_possessedPawn.Initialise(this);
		}

		public virtual void Dispose()
		{
			CommandInvoker.OnCommandExecutionStarted -= OnCommandExecutionStarted;
			CommandInvoker.OnCommandExecutionFinished -= OnCommandExecutionFinished;
			Destroy(_possessedPawn.gameObject);
			Destroy(gameObject);
		}
		
		public void SetPawn(PlayerPawn pawn) => _possessedPawn = pawn;

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

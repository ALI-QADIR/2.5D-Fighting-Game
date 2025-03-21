using Smash.Player.CommandPattern;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Smash.Player.Input
{
	[RequireComponent(typeof(ComboActionQueueManager))]
	public abstract class BaseController : MonoBehaviour
	{
		[Header("Components")]
		[field: SerializeField] protected ComboActionQueueManager ComboActionQueueManager { get; private set; }
		protected PlayerPawn _possessedPawn;
		protected GameplayActionCommandInvoker CommandInvoker { get; private set; }

		protected virtual void Awake()
		{
			ComboActionQueueManager ??= GetComponent<ComboActionQueueManager>();
		}

		public virtual void Initialise(PlayerPawn pawn)
		{
			_possessedPawn = pawn;
			_possessedPawn.Initialise(this);
			InitialiseCommandInvoker();
		}
		
		public void SetPawn(PlayerPawn pawn) => _possessedPawn = pawn;

		protected void InitialiseCommandInvoker()
		{
			CommandInvoker ??= new GameplayActionCommandInvoker();
		}

		public virtual void Dispose()
		{
			Destroy(_possessedPawn.gameObject);
			Destroy(gameObject);
		}
	}
}

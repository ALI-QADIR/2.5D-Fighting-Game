using Smash.Player.CommandPattern.ActionCommands;
using Smash.Player.CommandPattern.Controllers;
using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(ButtonActionController))]
	[RequireComponent(typeof(DPadActionController))]
	public class PlayerCharacterController : PlayerController
	{
		[field: SerializeField] private ButtonActionController ButtonActionControllerComponent { get; set; }
		[field: SerializeField] private DPadActionController DPadActionControllerComponent { get; set; }


		[Header("Debug")] 
		[SerializeField] private bool m_enableInvokerDebug = true;
		[SerializeField] private bool m_enableQueueDebug = true;

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
			ButtonActionControllerComponent ??= GetComponent<ButtonActionController>();
			DPadActionControllerComponent ??= GetComponent<DPadActionController>();
			
			CommandInvoker.Debugging = m_enableInvokerDebug;
			ComboQueueManager.Debugging = m_enableQueueDebug;
		}

		#endregion Unity Methods

		#region Public Methods

		public override void Initialise(CharacterPawn pawn)
		{
			base.Initialise(pawn);
			Initialise();
		}

		#endregion  Public Methods

		#region Private Methods

		protected override void Initialise()
		{
			base.Initialise();
			
			InitialiseActionControllers();
		}

		private void InitialiseActionControllers()
		{
			ButtonActionControllerComponent.Initialise(_inputActions, ComboQueueManager);
			DPadActionControllerComponent.Initialise(_inputActions, ComboQueueManager);
		}

		protected override void OnCommandExecutionStarted(IGameplayActionCommand command)
		{
			command.StartActionExecution(_inputHandler);
		}

		protected override void OnCommandExecutionFinished(IGameplayActionCommand command)
		{
			command.FinishActionExecution(_inputHandler);
		}

		#endregion Private Methods
	}
}

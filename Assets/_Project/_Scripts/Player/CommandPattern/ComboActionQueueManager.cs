using System.Collections.Generic;
using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class ComboActionQueueManager : MonoBehaviour
	{
		#region Feilds and Properties

		[SerializeField] private ComboActionCommandFactory m_comboActionCommandFactory;

		[SerializeField, Tooltip("Max time to wait since first command input to execute a combo"), Range(2, 10)]
		private float m_maxTimeForComboExecution = 3f;

		[SerializeField, Tooltip("Max time interval between each command input to be part of a combo"), Range(0.1f, 1f)]
		private float m_maxTimeBetweenCommands = 0.2f;

		[SerializeField, Range(2, 7)] private int m_maxQueueLengthBeforeTrim = 3;

		[SerializeField, Tooltip("Maximum Length possible for any combo"), Range(2, 5)]
		private int m_maxPossibleComboLength = 2;
		
		// [SerializeField, Tooltip("Time added to combo time with each new command input")]
		// private float m_comboTimeAddedWithNewCommand = 0.2f;

		public bool Debugging { get; set; }

		private bool m_hasStartedComboExecution;
		private int m_thresholdToExtendComboTime;
		private float m_timeSinceLastCommandInput;
		private float m_timeSinceFirstCommandInput;

		private Queue<IGameplayActionCommand> m_comboQueue;
		private ComboMatchEngine m_comboMatchEngine;
		private GameplayActionCommandInvoker m_commandInvoker;

		private IGameplayActionCommand m_currentCommand;
		
		private const int k_MinComboLength = 2;

		#endregion Feilds and Properties

		#region Unity Methods

		private void Start()
		{
			m_comboQueue = new Queue<IGameplayActionCommand>();
			m_comboMatchEngine = new ComboMatchEngine(this, m_comboActionCommandFactory, k_MinComboLength)
			{
				Debugging = Debugging
			};
		}

		private void Update()
		{
			ProcessComboUpdate(Time.deltaTime);
		}

		#endregion Unity Methods

		#region Public Methods
	
		public void SetCommandInvoker(GameplayActionCommandInvoker commandInvoker)
		{
			m_commandInvoker = commandInvoker;
		}

		public void TryExecuteCachedCombo()
		{
			if (m_comboQueue.Count == 0) return;
			m_currentCommand ??= m_comboMatchEngine.CheckAndGetComboCommand();
			FinishExecuteCommand();
		}

		public void AddCommandToComboSequence(IGameplayActionCommand command)
		{
			DebugLog($"Adding to sequence: {command.ActionName}");
			if (command is DPadNullActionCommand)
			{
				m_commandInvoker.InvokeFinishCommand(command);
				return;
			}
			EnqueueCommandAndResetTimers(command);
			ClearSequenceIfCannotStartCombo();
			DequeueOldestCommandIfNecessary();

			/*if (ShouldExtendComboTime())
			{
				// Debug.Log($"Extending Combo Time");
				m_timeSinceFirstCommandInput -= m_comboTimeAddedWithNewCommand;
			}*/
		}

		public Queue<IGameplayActionCommand> GetComboQueue() => m_comboQueue;

		#endregion Public Methods

		#region Private Methods

		private void FinishExecuteCommand()
		{
			// null command means no matching combo rule was found
			// hence the first input command is executed all others are ignored
			m_currentCommand ??= m_comboQueue.Peek();
			m_currentCommand.HeldDuration = m_timeSinceFirstCommandInput;
			m_commandInvoker.InvokeFinishCommand(m_currentCommand);
			ClearComboSequence();
		}

		private void StartExecuteCommand()
		{
			m_currentCommand ??= m_comboQueue.Peek();
			m_commandInvoker.InvokeStartCommand(m_currentCommand);
		}

		private void TryStartExecution()
		{
			m_hasStartedComboExecution = true;
			if (m_comboQueue.Count == 0) return;
			m_currentCommand = m_comboMatchEngine.CheckAndGetComboCommand();
			StartExecuteCommand();
		}

		private void ClearComboSequence()
		{
			m_currentCommand = null;
			m_hasStartedComboExecution = false;
			m_comboQueue.Clear();
			m_timeSinceLastCommandInput = 0;
			m_timeSinceFirstCommandInput = 0;
		}

		private void ClearSequenceIfCannotStartCombo()
		{
			// Debug.Log("Clearing sequence if we cannot start combo");
			if (m_comboQueue.Count == 0) return;
			bool canStartCombo = m_comboMatchEngine.CanStartCombo(m_comboQueue);
			if (canStartCombo) return;
			FinishExecuteCommand();
		}

		private void DequeueOldestCommandIfNecessary()
		{
			if (m_comboQueue.Count >= m_maxQueueLengthBeforeTrim)
			{
				// Debug.Log("Dequeue oldest command");
				m_comboQueue.Dequeue();
			}
		}

		private void EnqueueCommandAndResetTimers(IGameplayActionCommand command)
		{
			if (IsTooLateForCombo())
			{
				DebugLog($"Time Exceeded For Combo, ignoring command: {command.ActionName}");
				return;
			}

			if (m_comboQueue.Count >= m_maxPossibleComboLength)
			{
				DebugLog($"Max Possible Combo Length Reached, ignoring command: {command.ActionName}");
				return;
			}
			
			m_comboQueue.Enqueue(command);
			DebugLog($"Enqueued Command : {command.ActionName}, Current Queue Length {m_comboQueue.Count}");
			
			if (IsFirstCommandInSequence())
			{
				// Debug.Log($"Is first Command In Sequence");
				m_timeSinceFirstCommandInput = 0;
			}

			m_timeSinceLastCommandInput = 0;
		}

		private void ProcessComboUpdate(float deltaTime)
		{
			if (m_comboQueue.Count == 0) return;

			m_timeSinceLastCommandInput += deltaTime;
			m_timeSinceFirstCommandInput += deltaTime;
			
			if (IsTooLateForCombo() && !m_hasStartedComboExecution)
			{
				TryStartExecution();
			}

			if (m_timeSinceFirstCommandInput > m_maxTimeForComboExecution)
			{
				TryExecuteCachedCombo();
			}

			if (m_comboQueue.Count >= m_maxQueueLengthBeforeTrim)
			{
				// DebugLog("---===---===---Combo Queue is too long, trimming queue---===---===---");
				DequeueOldestCommandIfNecessary();
			}
		}

		/// <summary>
		/// Currently, we always extend the combo time with each new combo-valid input command.
		/// 
		/// This method can be adjusted to add more nuanced control over how and when
		/// the combo time window is extended, such as based on the type of command. 
		/// This allows for fine-tuning the responsiveness of the combo system to achieve the desired gameplay feel. 
		/// 
		/// For example, you could pass in the command like ShouldExtendComboTime(IGameplayActionCommand command)
		/// giving certain commands a stronger window extension by using the strategy pattern with the command.
		/// </summary>
		private bool ShouldExtendComboTime() => m_comboQueue.Count >= m_thresholdToExtendComboTime;

		private bool IsFirstCommandInSequence() => m_comboQueue.Count == 1;

		private bool IsTooLateForCombo() => m_timeSinceLastCommandInput > m_maxTimeBetweenCommands;

		private void DebugLog(string message)
		{
			if (!Debugging) return;
			Debug.Log(message);
		}

		#endregion Private Methods
	}
}

using System.Collections.Generic;
using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Smash.Player.CommandPattern
{
	public class ComboActionQueueManager : MonoBehaviour
	{
		[SerializeField] private ComboActionCommandFactory m_comboActionCommandFactory;
		[SerializeField] private float m_maxTimeForComboExecution = 0.5f;
		[SerializeField] private float m_minTimeBetweenCommands = 0.5f;
		[SerializeField] private float m_comboTimeAddedWithNewCommand;
		[SerializeField] private int m_maxComboLengthBeforeTrim = 6;
		
		private float m_timeSinceLastCommandInput;
		private float m_timeSinceFirstCommandInput;
		private int m_thresholdToExtendComboTime;
		
		private Queue<IGameplayActionCommand> m_comboQueue;
		private ComboMatchEngine m_comboMatchEngine;
		private GameplayActionCommandInvoker m_commandInvoker;

		private const int k_MinComboLength = 2;

		private void Start()
		{
			m_comboQueue = new Queue<IGameplayActionCommand>();
			m_comboMatchEngine = new ComboMatchEngine(this, m_comboActionCommandFactory, k_MinComboLength);
		}

		private void Update()
		{
			ProcessComboUpdate(Time.deltaTime);
		}
		
		public void SetCommandInvoker(GameplayActionCommandInvoker commandInvoker)
		{
			m_commandInvoker = commandInvoker;
		}
		
		public void ExecuteCommand(IGameplayActionCommand command)
		{
			m_commandInvoker.ExecuteCommand(command);
		}

		public void AddCommandToComboSequence(IGameplayActionCommand command)
		{
			Debug.LogWarning($"Adding to sequence: {command.ActionName}");
			ClearSequenceIfCannotStartCombo();
			DequeueOldestCommandIfNecessary();

			if (IsTooLateForCombo())
			{
				// Debug.Log($"Too Late For Combo");
				ClearComboSequence();
				return;
			}

			if (ShouldExtendComboTime())
			{
				// Debug.Log($"Extending Combo Time");
				m_timeSinceFirstCommandInput -= m_comboTimeAddedWithNewCommand;
			}

			EnqueueCommandAndResetTimers(command);
			m_comboMatchEngine.CheckAndExecuteCombo();
		}
		
		public Queue<IGameplayActionCommand> GetComboQueue() => m_comboQueue;

		public void ClearComboSequence()
		{
			m_comboQueue.Clear();
			m_timeSinceLastCommandInput = 0;
			m_timeSinceFirstCommandInput = 0;
		}

		private void ClearSequenceIfCannotStartCombo()
		{
			// Debug.Log("Clearing sequence if we cannot start combo");
			// bool canStartCombo = m_comboMatchEngine.CanStartCombo(m_comboQueue);
			if (m_comboQueue.Count > 0 && !m_comboMatchEngine.CanStartCombo(m_comboQueue))
			{
				// Debug.Log("Clearing sequence");
				ClearComboSequence();
			}
		}

		private void DequeueOldestCommandIfNecessary()
		{
			if (m_comboQueue.Count >= m_maxComboLengthBeforeTrim)
			{
				// Debug.Log("Dequeue oldest command");
				m_comboQueue.Dequeue();
			}
		}
		
		private void EnqueueCommandAndResetTimers(IGameplayActionCommand command)
		{
			m_comboQueue.Enqueue(command);
			Debug.LogWarning($"Enqueued Command : {command.ActionName}, Current Queue Length {m_comboQueue.Count}");
			if (IsFirstCommandInSequence())
			{
				// Debug.Log($"Is first Command In Sequence");
				m_timeSinceFirstCommandInput = 0;
			}

			m_timeSinceLastCommandInput = 0;
		}

		private bool IsTooLateForCombo()
		{
			if (m_timeSinceLastCommandInput < m_minTimeBetweenCommands) return false;
			return true;
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
		private bool ShouldExtendComboTime()
		{
			return m_comboQueue.Count >= m_thresholdToExtendComboTime;
		}
		
		private bool IsFirstCommandInSequence() => m_comboQueue.Count == 1;

		private void ProcessComboUpdate(float deltaTime)
		{
			if(m_comboQueue.Count == 0) return;
			
			m_timeSinceLastCommandInput += deltaTime;
			m_timeSinceFirstCommandInput += deltaTime;

			if (m_timeSinceFirstCommandInput > m_maxTimeForComboExecution)
				ClearComboSequence();
		}
	}
}

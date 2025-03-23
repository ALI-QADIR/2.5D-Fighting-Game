using System.Collections.Generic;
using System.Linq;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.Player.CommandPattern.ComboRules;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class ComboMatchEngine
	{
		private readonly ComboActionQueueManager m_manager;
		private readonly ComboActionCommandFactory m_factory;
		private readonly int m_minComboLength;
		private readonly List<IComboRule> comboRules;
		public bool Debugging { get; set; }
		
		public ComboMatchEngine(ComboActionQueueManager manager, ComboActionCommandFactory factory, int minComboLength)
		{
			m_manager = manager;
			m_factory = factory;
			m_minComboLength = minComboLength;
			
			comboRules = new List<IComboRule>()
			{
				new ComboSideSpecialRule(m_factory),
				new ComboUpSpecialRule(m_factory),
				new ComboDownSpecialRule(m_factory),
				new ComboSideMainRule(m_factory),
				new ComboUpMainRule(m_factory),
				new ComboDownMainRule(m_factory)
			};
		}

		public bool CanStartCombo(Queue<IGameplayActionCommand> comboQueue)
		{
			DebugLog($"Can Start Combo {DoesFirstActionStartCombo(comboQueue, comboRules)}");
			return DoesFirstActionStartCombo(comboQueue, comboRules);
		}

		public void CheckAndExecuteCombo()
		{
			// Debug.Log("Check And Execute Combo");
			var comboQueue = m_manager.GetComboQueue();
			if (comboQueue.Count < m_minComboLength)
				return;

			// Debug.Log("Appropriate Combo Length");
			IGameplayActionCommand comboCommand = CheckQueueForCombo(comboQueue);
			if (comboCommand == null) return;
			m_manager.ExecuteCommand(comboCommand);
			// ComboEventSystem.OnNewCombo?.Invoke();
			m_manager.ClearComboSequence();
		}

		private IGameplayActionCommand CheckQueueForCombo(Queue<IGameplayActionCommand> comboQueue)
		{
			for (int startIndex = 0; startIndex <= comboQueue.Count; startIndex++)
			{
				var subsequence = GetSubsequence(comboQueue, startIndex).ToArray();

				foreach (var rule in comboRules)
				{
					// Debug.Log($"Checking rule {rule.RuleName}");
					if (rule.IsMatch(subsequence))
					{
						return rule.GetResultingComboCommand();
					}
				}
			}

			return null;
		}
		
		private void ExecuteComboAction(IGameplayActionCommand comboCommand)
		{
			comboCommand.ExecuteAction();
		}
		
		private static IEnumerable<IGameplayActionCommand> GetSubsequence(Queue<IGameplayActionCommand> comboQueue, int startIndex)
		{
			return comboQueue.Skip(startIndex);
		}

		private static bool DoesFirstActionStartCombo(Queue<IGameplayActionCommand> comboQueue,
			List<IComboRule> comboRules)
		{
			foreach (var rule in comboRules)
			{
				if (rule.IsFirstConditionMet(comboQueue.Peek()))
					return true;
			}

			return false;
		}
		
		private void DebugLog(string message)
		{
			if (!Debugging) return;
			Debug.LogWarning(message);
		}
	}
}

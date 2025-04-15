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
		private readonly int m_minComboLength;
		private readonly List<IComboRule> comboRules;
		public bool Debugging { get; set; }
		
		public ComboMatchEngine(ComboActionQueueManager manager, ComboActionCommandFactory factory, int minComboLength)
		{
			m_manager = manager;
			m_minComboLength = minComboLength;
			
			comboRules = new List<IComboRule>
			{
				new ComboSideSpecialRule(factory),
				new ComboUpSpecialRule(factory),
				new ComboDownSpecialRule(factory),
				new ComboSideMainRule(factory),
				new ComboUpMainRule(factory),
				new ComboDownMainRule(factory)
			};
		}

		public bool CanStartCombo(Queue<IGameplayActionCommand> comboQueue)
		{
			bool canStartCombo = DoesFirstActionStartCombo(comboQueue, comboRules);
			DebugLog($"Can Start Combo: {canStartCombo}");
			return canStartCombo;
		}

		public bool ShouldEnqueueCommand(IGameplayActionCommand command)
		{
			return SecondActionMakesCombo(command, comboRules);
		}

		public IGameplayActionCommand CheckAndGetComboCommand()
		{
			// Debug.Log("Check And Execute Combo");
			var comboQueue = m_manager.GetComboQueue(); 
			// DebugLog("Appropriate Combo Length");
			IGameplayActionCommand comboCommand = CheckQueueForCombo(comboQueue);
			return comboCommand;
			// ComboEventSystem.OnNewCombo?.Invoke();
		}

		private IGameplayActionCommand CheckQueueForCombo(Queue<IGameplayActionCommand> comboQueue)
		{
			if (comboQueue.Count < m_minComboLength)
				return null;
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
		
		private static IEnumerable<IGameplayActionCommand> GetSubsequence(Queue<IGameplayActionCommand> comboQueue, int startIndex)
		{
			return comboQueue.Skip(startIndex);
		}

		private static bool DoesFirstActionStartCombo(Queue<IGameplayActionCommand> comboQueue, List<IComboRule> comboRules)
		{
			foreach (var rule in comboRules)
			{
				if (rule.IsFirstConditionMet(comboQueue.Peek()))
					return true;
			}

			return false;
		}

		private static bool SecondActionMakesCombo(IGameplayActionCommand command, List<IComboRule> comboRules)
		{
			foreach (var rule in comboRules)
			{
				if (rule.IsSecondConditionMet(command))
					return true;
			}

			return false;
		}
		
		private void DebugLog(string message)
		{
			if (!Debugging) return;
			Debug.Log(message);
		}
	}
}

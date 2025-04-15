using System;
using System.Collections.Generic;
using System.Linq;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern.ComboRules
{
	public abstract class ComboSpecialRule : IComboRule
	{
		protected readonly ComboActionCommandFactory _factory;

		public abstract string RuleName { get; }
		
		protected Type[] _compositeCommands;
		
		public int ComboLength { get; protected set; }
		
		protected ComboSpecialRule(ComboActionCommandFactory factory)
		{
			_factory = factory;
		}
		
		public abstract bool IsFirstConditionMet(IGameplayActionCommand firstCommand);

		public virtual bool IsSecondConditionMet(IGameplayActionCommand secondCommand)
		{
			return secondCommand.GetType() == _compositeCommands[1];
		}

		public virtual bool IsMatch(IEnumerable<IGameplayActionCommand> sequence)
		{
			var sequenceArray = sequence.Take(ComboLength).ToArray();
			if (sequenceArray.Length < ComboLength)
				return false;
			
			bool isMatch = true;
			for (int i = 0; i < ComboLength; i++)
			{
				isMatch = isMatch && sequenceArray[i].GetType() == _compositeCommands[i];
				// Debug.Log($"{sequenceArray[i].GetType()} == {_compositeCommands[i]}? {isMatch}");
			}

			return isMatch;
		}
		
		public abstract IGameplayActionCommand GetResultingComboCommand();
	}
}

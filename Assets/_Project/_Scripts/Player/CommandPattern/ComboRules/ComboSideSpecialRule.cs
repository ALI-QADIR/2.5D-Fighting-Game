using System;
using System.Collections.Generic;
using System.Linq;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern.ComboRules
{
	public class ComboSideSpecialRule : ComboSpecialRule
	{
		public override string RuleName { get; } = "Combo Side Special Rule";

		private int m_direction;
		
		public ComboSideSpecialRule(ComboActionCommandFactory factory) : base(factory)
		{
			ComboLength = 2;
			_compositeCommands = new Type[] { };
		}
		
		public override bool IsFirstConditionMet(IGameplayActionCommand firstCommand)
		{
			return firstCommand is SouthButtonActionCommand;
		}
		
		public override bool IsSecondConditionMet(IGameplayActionCommand secondCommand)
		{
			return secondCommand is DPadLeftActionCommand or DPadRightActionCommand;
		}


		public override bool IsMatch(IEnumerable<IGameplayActionCommand> sequence)
		{
			var sequenceArray = sequence.Take(ComboLength).ToArray();
			if (sequenceArray.Length < ComboLength)
				return false;
			
			var firstCommand = sequenceArray[0];
			var secondCommand = sequenceArray[1];
			
			m_direction = secondCommand is DPadLeftActionCommand ? -1 : 1;

			return firstCommand is SouthButtonActionCommand && secondCommand is DPadLeftActionCommand or DPadRightActionCommand;
		}

		public override IGameplayActionCommand GetResultingComboCommand()
		{
			return _factory.CreateSideSpecialCommand(m_direction);
		}
	}
}

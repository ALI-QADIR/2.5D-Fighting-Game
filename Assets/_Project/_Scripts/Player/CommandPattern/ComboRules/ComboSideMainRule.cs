using System;
using System.Collections.Generic;
using System.Linq;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern.ComboRules
{
	public class ComboSideMainRule : ComboSpecialRule
	{
		public override string RuleName { get; } = "Combo Side Main Rule";
		
		public ComboSideMainRule(ComboActionCommandFactory factory) : base(factory)
		{
			ComboLength = 2;
			_compositeCommands = new Type[] { };
		}
		
		public override bool IsFirstConditionMet(IGameplayActionCommand firstCommand)
		{
			return firstCommand is EastButtonActionCommand;
		}

		public override bool IsMatch(IEnumerable<IGameplayActionCommand> sequence)
		{
			var sequenceArray = sequence.Take(ComboLength).ToArray();
			if (sequenceArray.Length < ComboLength)
				return false;
			
			var firstCommand = sequenceArray[0];
			var secondCommand = sequenceArray[1];

			return firstCommand is EastButtonActionCommand && secondCommand is DPadLeftActionCommand or DPadRightActionCommand;
		}

		public override IGameplayActionCommand GetResultingComboCommand()
		{
			return _factory.CreateSideMainCommand();
		}
	}
}

using System;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern.ComboRules
{
	public class ComboUpSpecialRule : ComboSpecialRule
	{
		public override string RuleName { get; } = "Combo Up Special Rule";
		
		public ComboUpSpecialRule(ComboActionCommandFactory factory) : base(factory)
		{
			ComboLength = 2;
			_compositeCommands = new Type[]
			{
				typeof(SouthButtonActionCommand),
				typeof(DPadUpActionCommand)
			};
		}
		
		public override bool IsFirstConditionMet(IGameplayActionCommand firstCommand)
		{
			return firstCommand is SouthButtonActionCommand;
		}

		public override IGameplayActionCommand GetResultingComboCommand()
		{
			return _factory.CreateUpSpecialCommand();
		}
	}
}

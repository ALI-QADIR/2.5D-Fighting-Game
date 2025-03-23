using System;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern.ComboRules
{
	public class ComboUpMainRule : ComboSpecialRule
	{
		public override string RuleName { get; } = "Combo Up Main Rule";
		
		public ComboUpMainRule(ComboActionCommandFactory factory) : base(factory)
		{
			ComboLength = 2;
			_compositeCommands = new Type[]
			{
				typeof(EastButtonActionCommand),
				typeof(DPadUpActionCommand)
			};
		}
		
		public override bool IsFirstConditionMet(IGameplayActionCommand firstCommand)
		{
			return firstCommand is EastButtonActionCommand;
		}
		
		public override IGameplayActionCommand GetResultingComboCommand()
		{
			return _factory.CreateUpMainCommand();
		}
	}
}

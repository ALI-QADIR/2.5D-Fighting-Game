using System;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern.ComboRules
{
	public class ComboDownMainRule : ComboSpecialRule
	{
		public override string RuleName { get; } = "Combo Down Main Rule";
		
		public ComboDownMainRule(ComboActionCommandFactory factory) : base(factory)
		{
			ComboLength = 2;
			_compositeCommands = new Type[]
			{
				typeof(EastButtonActionCommand),
				typeof(DPadDownActionCommand)
			};
		}
		
		public override bool IsFirstConditionMet(IGameplayActionCommand firstCommand)
		{
			return firstCommand is EastButtonActionCommand;
		}
		
		public override IGameplayActionCommand GetResultingComboCommand()
		{
			return _factory.CreateDownMainCommand();
		}
	}
}

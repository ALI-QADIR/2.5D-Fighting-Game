using System;
using Smash.Player.CommandPattern.ActionCommands;

namespace Smash.Player.CommandPattern.ComboRules
{
	public class ComboDownSpecialRule : ComboSpecialRule
	{
		public override string RuleName { get; } = "Combo Down Special Rule";
		
		public ComboDownSpecialRule(ComboActionCommandFactory factory) : base(factory)
		{
			ComboLength = 2;
			_compositeCommands = new Type[]
			{
				typeof(SouthButtonActionCommand),
				typeof(DPadDownActionCommand)
			};
		}
		
		public override bool IsFirstConditionMet(IGameplayActionCommand firstCommand)
		{
			return firstCommand is SouthButtonActionCommand;
		}

		public override IGameplayActionCommand GetResultingComboCommand()
		{
			return _factory.CreateDownSpecialCommand();
		}
	}
}

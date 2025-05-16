using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class ComboActionCommandFactory : MonoBehaviour
	{
		public IGameplayActionCommand CreateSideSpecialCommand(int direction)
		{
			// Debug.Log("Create Side Special Command");
			return new ComboSideSpecialCommand(direction);
		}
		
		public IGameplayActionCommand CreateDownSpecialCommand()
		{
			// Debug.Log("Create Down Special Command");
			return new ComboDownSpecialCommand();
		}
		
		public IGameplayActionCommand CreateUpSpecialCommand()
		{
			// Debug.Log("Create Up Special Command");
			return new ComboUpSpecialCommand();
		}

		public IGameplayActionCommand CreateSideMainCommand(int direction)
		{
			return new ComboSideMainCommand(direction);
		}
		
		public IGameplayActionCommand CreateDownMainCommand()
		{
			return new ComboDownMainCommand();
		}
		
		public IGameplayActionCommand CreateUpMainCommand()
		{
			return new ComboUpMainCommand();
		}
	}
}

using Smash.Player.CommandPattern.ActionCommands;
using UnityEngine;

namespace Smash.Player.CommandPattern
{
	public class ComboActionCommandFactory : MonoBehaviour
	{
		public IGameplayActionCommand CreateSideSpecialCommand()
		{
			// Debug.Log("Create Side Special Command");
			return new ComboSideSpecialCommand();
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
	}
}

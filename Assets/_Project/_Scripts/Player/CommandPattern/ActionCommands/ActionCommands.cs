namespace Smash.Player.CommandPattern.ActionCommands
{
	public class NorthButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "North";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}

		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.HandleJumpInput();
		}
	}
	
	public class SouthButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "South";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
		}
	}
	
	public class EastButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "East";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
		}
	}
	
	public class WestButtonActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "West";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.HandleJumpInput();
		}
	}
	
	public class DPadUpActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadUp";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.HandleUpInput();
		}
	}
	
	public class DPadDownActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadDown";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.HandleDownInput();
		}
	}
	
	public class DPadLeftActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadLeft";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.Direction = UnityEngine.Vector3.left;
		}
	}
	
	public class DPadRightActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadRight";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.Direction = UnityEngine.Vector3.right;
		}
	}
	
	public class DPadNullActionCommand : IGameplayActionCommand
	{
		public string ActionName { get; } = "DPadNull";
		public float HeldDuration { get; set; }
		
		public void StartActionExecution(PlayerPawn pawn)
		{
		}
		
		public void FinishActionExecution(PlayerPawn pawn)
		{
			pawn.Direction = UnityEngine.Vector3.zero;
		}
	}
}

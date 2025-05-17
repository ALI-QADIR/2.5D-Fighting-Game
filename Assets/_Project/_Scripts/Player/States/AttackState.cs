using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.States
{
	// TODO: later check if the attack state logic is same, then rather than using so many separate methods for each attack state, pass in the type of the attack state
	public class MainAttackStart : PlayerBaseState
	{
		public MainAttackStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetMainAttackWindup();
			_graphicsController.SetMainAttackWindUp();
		}
	}
	
	public class MainAttackEnd : PlayerBaseState
	{
		public float ElapsedTime { get; private set; }
		
		public MainAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			ElapsedTime = 0f;
			_pawn.SetMainAttackExecute();
			_graphicsController.SetMainAttackFinish();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetMainAttackFinish();
			ElapsedTime = 0f;
		}
	}

	public class SideMainAttackStart : PlayerBaseState
	{
		public SideMainAttackStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSideMainAttackWindUp();
		}
	}
	
	public class SideMainAttackEnd : PlayerBaseState
	{
		public SideMainAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSideMainAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetSideMainAttackFinish();
		}
	}
	
	public class UpMainAttackStart : PlayerBaseState
	{
		public UpMainAttackStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetUpMainAttackWindUp();
		}
	}
	
	public class UpMainAttackEnd : PlayerBaseState
	{
		public UpMainAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetUpMainAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetUpMainAttackFinish();
		}
	}
	
	public class DownMainAttackStart : PlayerBaseState
	{
		public DownMainAttackStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetDownMainAttackWindUp();
		}
	}
	
	public class DownMainAttackEnd : PlayerBaseState
	{
		public DownMainAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetDownMainAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetDownMainAttackFinish();
		}
	}
	
	public class SpecialAttackStart : PlayerBaseState
	{
		public SpecialAttackStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}	
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSpecialAttackWindup();
			_graphicsController.SetSpecialAttackWindUp();
		}
	}
	
	public class SpecialAttackEnd : PlayerBaseState
	{
		public float ElapsedTime { get; private set; }

		public SpecialAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			ElapsedTime = 0;
			_pawn.SetSpecialAttackExecute();
			_graphicsController.SetSpecialAttackFinish();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}

		public override void OnExit()
		{
			base.OnExit();
			ElapsedTime = 0;
			_pawn.SetSpecialAttackFinish();
		}
	}
	
	public class SideSpecialAttackStart : PlayerBaseState
	{
		public SideSpecialAttackStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSideSpecialAttackWindUp();
		}
	}
	
	public class SideSpecialAttackEnd : PlayerBaseState
	{
		public SideSpecialAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetSideSpecialAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetSideSpecialAttackFinish();
		}
	}

	public class UpSpecialAttackStart : PlayerBaseState
	{
		public UpSpecialAttackStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetUpSpecialAttackWindUp();
		}
	}
	
	public class UpSpecialAttackEnd : PlayerBaseState
	{
		public UpSpecialAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetUpSpecialAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetUpSpecialAttackFinish();
		}
	}
	
	public class DownSpecialAttackStart : PlayerBaseState
	{
		public DownSpecialAttackStart(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetDownSpecialAttackWindUp();
		}
	}
	
	public class DownSpecialAttackEnd : PlayerBaseState
	{
		public DownSpecialAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetDownSpecialAttackExecute();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetDownSpecialAttackFinish();
		}
	}
}

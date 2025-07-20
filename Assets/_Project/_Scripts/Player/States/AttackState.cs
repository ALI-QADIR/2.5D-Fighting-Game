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
			_pawn.mainAbilityStrategy.OnEnter();
			_graphicsController.SetMainAttackFinish();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}
		
		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			_pawn.mainAbilityStrategy.OnFixedUpdate();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetMainAttackFinish();
			_pawn.mainAbilityStrategy.OnExit();
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
			_graphicsController.SetSideMainAttackWindUp();
		}
	}
	
	public class SideMainAttackEnd : PlayerBaseState
	{
		public float ElapsedTime { get; private set; }
		
		public SideMainAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			ElapsedTime = 0f;
			_pawn.SetSideMainAttackExecute();
			_graphicsController.SetSideMainAttackFinish();
			_pawn.sideMainAbilityStrategy.OnEnter();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}

		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			_pawn.sideMainAbilityStrategy.OnFixedUpdate();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetSideMainAttackFinish();
			_pawn.sideMainAbilityStrategy.OnExit();
			ElapsedTime = 0;
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
			_graphicsController.SetUpMainAttackWindUp();
		}
	}
	
	public class UpMainAttackEnd : PlayerBaseState
	{
		public float ElapsedTime { get; private set; }
		public UpMainAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			ElapsedTime = 0f;
			_pawn.SetUpMainAttackExecute();
			_graphicsController.SetUpMainAttackFinish();
			_pawn.upMainAbilityStrategy.OnEnter();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}

		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			_pawn.upMainAbilityStrategy.OnFixedUpdate();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetUpMainAttackFinish();
			_pawn.upMainAbilityStrategy.OnExit();
			ElapsedTime = 0;
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
			_pawn.specialAbilityStrategy.OnEnter();
			_graphicsController.SetSpecialAttackFinish();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}

		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			_pawn.specialAbilityStrategy.OnFixedUpdate();
		}

		public override void OnExit()
		{
			base.OnExit();
			ElapsedTime = 0;
			_pawn.SetSpecialAttackFinish();
			_pawn.specialAbilityStrategy.OnExit();
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
			_graphicsController.SetUpSpecialAttackWindUp();
		}
	}
	
	public class UpSpecialAttackEnd : PlayerBaseState
	{
		public float ElapsedTime { get; private set; }
		public UpSpecialAttackEnd(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
		
		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			ElapsedTime = 0;
			_pawn.SetUpSpecialAttackExecute();
			_pawn.upSpecialAbilityStrategy.OnEnter();
			_graphicsController.SetUpSpecialAttackFinish();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}

		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			_pawn.upSpecialAbilityStrategy.OnFixedUpdate();
		}

		public override void OnExit()
		{
			base.OnExit();
			ElapsedTime = 0;
			_pawn.SetUpSpecialAttackFinish();
			_pawn.upSpecialAbilityStrategy.OnExit();
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

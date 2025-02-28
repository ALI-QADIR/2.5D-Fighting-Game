using UnityEngine;

namespace Smash.Player.States
{
	public class AirEntry : PlayerBaseState
	{
		public AirEntry(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.SetInAir();
		}
	}
	
	public class Rising : PlayerBaseState
	{
		public Rising(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetInAir();
		}
	}
	
	public class Falling : PlayerBaseState
	{
		public Falling(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetFalling();
		}
	}
	public class FloatingFall : PlayerBaseState
	{
		public FloatingFall(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetFloatingFall();
		}
	}
	
	public class CrashingFall : PlayerBaseState
	{
		public CrashingFall(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetCrashingFall();
		}
	}
	
	public class Coyote : PlayerBaseState
	{
		public float ElapsedTime { get; private set; }

		public Coyote(PlayerPawn pawn) : base(pawn)
		{
		}


		public override void OnEnter()
		{
			base.OnEnter();
			ElapsedTime = 0f;
			_pawn.CurrentState = this;
			_pawn.SetCoyote();
		}
		
		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}
	}

	public class Apex : PlayerBaseState
	{
		public float ElapsedTime { get; private set; }

		public Apex(PlayerPawn pawn) : base(pawn)
		{
		}


		public override void OnEnter()
		{
			base.OnEnter();
			ElapsedTime = 0f;
			_pawn.CurrentState = this;
			_pawn.SetApex(true);
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}

		public override void OnExit()
		{
			base.OnExit();
			ElapsedTime = 0f;
			_pawn.SetApex(false);
		}
	}

	public class Ledge : PlayerBaseState
	{
		public Ledge(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetOnLedge(true);
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetOnLedge(false);
		}
	}
	
	public class WallSlide : PlayerBaseState
	{
		public WallSlide(PlayerPawn pawn) : base(pawn)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetWallSliding(true);
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetWallSliding(false);
		}
	}
    
	public class AirExit : PlayerBaseState
	{
		public AirExit(PlayerPawn pawn) : base(pawn)
		{
		}
	}
}

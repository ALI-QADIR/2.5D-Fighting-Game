using Smash.Player.Components;
using UnityEngine;

namespace Smash.Player.States
{
	public class AirState : PlayerBaseState
	{
		protected AirState(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
	}
	
	public class AirEntry : AirState
	{
		public AirEntry(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.SetInAir();
		}
	}
	
	public class Rising : AirState
	{
		public Rising(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetInAir();
			// if(_pawn.IsClimbing()) _graphicsController.SetClimbing();
			// else _graphicsController.SetJumping();
			_graphicsController.SetJumping();
		}
	}
	
	public class Falling : AirState
	{
		public Falling(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetFalling();
			_graphicsController.SetFalling();
		}
	}
	
	public class Coyote : AirState
	{
		public float ElapsedTime { get; private set; }

		public Coyote(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}


		public override void OnEnter()
		{
			base.OnEnter();
			ElapsedTime = 0f;
			_pawn.CurrentState = this;
			_graphicsController.SetFalling();
		}
		
		public override void OnUpdate()
		{
			base.OnUpdate();
			ElapsedTime += Time.deltaTime;
		}
	}

	public class Apex : AirState
	{
		public float ElapsedTime { get; private set; }

		public Apex(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
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

	/*public class Ledge : AirState
	{
		public Ledge(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_pawn.CurrentState = this;
			_pawn.SetOnLedge(true);
			_graphicsController.SetOnLedge();
		}

		public override void OnExit()
		{
			base.OnExit();
			_pawn.SetOnLedge(false);
		}
	}*/
	
	public class WallSlide : AirState
	{
		public WallSlide(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
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
    
	public class AirExit : AirState
	{
		public AirExit(CharacterPawn pawn, PlayerGraphicsController graphicsController) : base(pawn, graphicsController)
		{
		}
	}
}

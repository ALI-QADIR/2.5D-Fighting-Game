using TripleA.StateMachine.FSM;

namespace Smash.System
{

	public class SpeedRunInit : SpeedRunState
	{
		public SpeedRunInit(SpeedRunManager manager) : base(manager)
		{
		}
	}
	
	public class SpeedRunGetReady : SpeedRunState
	{
		public SpeedRunGetReady(SpeedRunManager manager) : base(manager)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_manager.OpenStartPanel();
		}
		
	}

	public class SpeedRunCountdown : SpeedRunState
	{
		public SpeedRunCountdown(SpeedRunManager manager) : base(manager)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_manager.BeginCountDown();
		}
	}
	
	public class SpeedRunActive : SpeedRunState
	{
		public SpeedRunActive(SpeedRunManager manager) : base(manager)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_manager.GameStarted();
		}
	}
	
	public class SpeedRunEnd : SpeedRunState
	{
		public SpeedRunEnd(SpeedRunManager manager) : base(manager)
		{
		}

		public override void OnEnter()
		{
			base.OnEnter();
			_manager.GameEnded();
		}
	}

	public class SpeedRunState : BaseState
	{
		protected readonly SpeedRunManager _manager;

		protected SpeedRunState(SpeedRunManager manager)
		{
			_manager = manager;
		}
	}
}

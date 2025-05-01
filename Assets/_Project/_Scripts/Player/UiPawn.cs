namespace Smash.Player
{
	public class UiPawn : BasePawn
	{
		public override void Initialise(BaseController possessingController)
		{
			base.Initialise(possessingController);
			_inputHandler.SetUiPawn(this);
		}
	}
}

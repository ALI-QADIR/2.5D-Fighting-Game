namespace Smash.Player.Components
{
	public class NullScanner : Scanner
	{
		public override void Scan() { }

		public override void Emit()
		{
			context.ApplyEffects(context.ownerCollider);
		}
	}
}

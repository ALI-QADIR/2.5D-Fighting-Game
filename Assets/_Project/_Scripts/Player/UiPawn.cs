using Smash.StructsAndEnums;
using Smash.System;
using TripleA.Utils.Observables.Primaries;

namespace Smash.Player
{
	public class UiPawn : BasePawn
	{
		private ObservableBool m_backHeld;
		public float BackHeldTime { get; set; }

		public override void Initialise()
		{
			// no-op
			// _inputHandler.SetUiPawn(this);
			m_backHeld = new ObservableBool(false);
		}

		public void BackHeld(bool held)
		{
			m_backHeld.Set(held);
			if (!held && SelectionManager.HasInstance)
			{
				AsyncSceneLoader.Instance.LoadSceneByType(MySceneTypes.MainMenu);
			}
		}
	}
}

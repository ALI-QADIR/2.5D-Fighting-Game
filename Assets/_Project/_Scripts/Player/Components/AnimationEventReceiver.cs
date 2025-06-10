using TripleA.Utils.Extensions;
using UnityEngine;

namespace Smash.Player.Components
{
	public class AnimationEventReceiver : TripleA.AnimationEvents.AnimationEventReceiver
	{
		[SerializeField] private CharacterPawn m_pawn;

		private void Awake()
		{
			m_pawn ??= gameObject.GetOrAddComponent<CharacterPawn>();
		}

		public void BeginAttack()
		{
			m_pawn.mainAttackStrategy.CanAttack = true;
		}

		public void EndAttack()
		{
			m_pawn.mainAttackStrategy.CanAttack = false;
		}
	}
}

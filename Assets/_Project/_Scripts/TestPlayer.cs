using Smash.Player;
using UnityEngine;

namespace Smash
{
	public class TestPlayer : MonoBehaviour
	{
		private void Start()
		{
			var pawn = GetComponent<CharacterPawn>();
			pawn.SetIndex(-1);
			pawn.Initialise();
		}
	}
}

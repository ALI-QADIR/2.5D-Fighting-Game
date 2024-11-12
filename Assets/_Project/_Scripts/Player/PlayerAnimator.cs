using System;
using Smash.Player.States;
using UnityEngine;

namespace Smash.Player
{
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField] private Animator m_animator;

		private void Awake()
		{
			if (m_animator == null)
			{
				throw new ArgumentNullException(nameof(m_animator));
			}
		}

		public void SetState(Type state)
		{
			if (state == typeof(GroundedSubStateMachine) || state == typeof(AirborneSubStateMachine)) return; 
		}
	}
}
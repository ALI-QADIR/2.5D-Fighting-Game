using System;
using UnityEngine;

namespace Smash.Player
{
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField] private Animator m_animator;
		[SerializeField] private string m_idleAnimationName = "Idle";
		[SerializeField] private string m_runningAnimationName = "Running";
		[SerializeField] private string m_dashAnimationName = "Dash";
		[SerializeField] private string m_fallingAnimationName = "Falling";
		[SerializeField] private string m_ledgeAnimationName = "Ledge";
		[SerializeField] private string m_jumpingAnimationName = "Jump";
		[SerializeField] private string m_flipAnimationName = "Flip";
		[SerializeField] private string m_climbAnimationName = "Climb";

		private int m_jumps;

		private int m_idleAnimationHash;
		private int m_runningAnimationHash;
		private int m_dashAnimationHash;
		private int m_fallingAnimationHash;
		private int m_ledgeAnimationHash;
		private int m_jumpingAnimationHash;
		private int m_flipAnimationHash;
		private int m_climbAnimationHash;

		private void Awake()
		{
			if (m_animator == null)
			{
				throw new ArgumentNullException(nameof(m_animator));
			}

			m_idleAnimationHash = Animator.StringToHash(m_idleAnimationName);
			m_runningAnimationHash = Animator.StringToHash(m_runningAnimationName);
			m_dashAnimationHash = Animator.StringToHash(m_dashAnimationName);
			m_fallingAnimationHash = Animator.StringToHash(m_fallingAnimationName);
			m_ledgeAnimationHash = Animator.StringToHash(m_ledgeAnimationName);
			m_jumpingAnimationHash = Animator.StringToHash(m_jumpingAnimationName);
			m_flipAnimationHash = Animator.StringToHash(m_flipAnimationName);
			m_climbAnimationHash = Animator.StringToHash(m_climbAnimationName);
		}

		public void SetOnGround()
		{
			m_jumps = 0;
		}

		public void SetIdle() => m_animator.CrossFadeInFixedTime(m_idleAnimationHash, 0.1f);

		public void SetRunning() => m_animator.CrossFadeInFixedTime(m_runningAnimationHash, 0.05f);
		public void SetDashing()
		{
			m_animator.CrossFadeInFixedTime(m_dashAnimationHash, 0.1f);
			m_jumps = 1;
		}

		public void SetFalling()
		{
			if (m_jumps > 1) return;
			m_animator.CrossFadeInFixedTime(m_fallingAnimationHash, 0.1f);
		}

		public void SetOnLedge()
		{
			m_animator.CrossFadeInFixedTime(m_ledgeAnimationHash, 0.1f);
			m_jumps = 1;
		}

		public void SetJumping()
		{
			m_jumps++;
			if (m_jumps <=1)
				m_animator.CrossFadeInFixedTime(m_jumpingAnimationHash, 0.1f);
			else 
				m_animator.CrossFadeInFixedTime(m_flipAnimationHash, 0.1f);
		}

		public void SetClimbing() => m_animator.CrossFadeInFixedTime(m_climbAnimationHash, 0.1f);
	}
}
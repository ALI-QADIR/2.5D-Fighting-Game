using System;
using Smash.StructsAndEnums;
using UnityEngine;

namespace Smash.Player.Components
{
	public class PlayerGraphicsController : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private Animator m_animator;
		
		private int m_groundEntryBoolHash;
		private int m_ledgeGrabTriggerHash;
		private int m_climbTriggerHash;
		private int m_isMovingBoolHash;
		private int m_isRisingBoolHash;
		private int m_isFallingBoolHash;
		private int m_tossUpTriggerHash;
		
		private int m_knockBackTriggerHash;
		private int m_knockBackDirectionParamHash;
		private int m_knockBackAnimSpeedParamHash;
		
		private int m_mainAttackStartTriggerHash;
		private int m_mainAttackFinishTriggerHash;

		private int m_sideMainAttackStartTriggerHash;
		private int m_sideMainAttackFinishTriggerHash;
		
		private int m_upMainAttackStartTriggerHash;
		private int m_upMainAttackFinishTriggerHash;
		
		private int m_specialAttackStartTriggerHash;
		private int m_specialAttackFinishTriggerHash;
		
		private int m_upSpecialAttackStartTriggerHash;
		private int m_upSpecialAttackFinishTriggerHash;
		
		private void Awake()
		{
			
			m_groundEntryBoolHash = Animator.StringToHash("GroundEntry");
			m_ledgeGrabTriggerHash = Animator.StringToHash("LedgeGrab");
			m_climbTriggerHash = Animator.StringToHash("Climb");
			m_isMovingBoolHash = Animator.StringToHash("IsMoving");
			m_isRisingBoolHash = Animator.StringToHash("IsRising");
			m_isFallingBoolHash = Animator.StringToHash("IsFalling");
			m_tossUpTriggerHash = Animator.StringToHash("TossUp");
			
			m_knockBackTriggerHash = Animator.StringToHash("KnockBack");
			m_knockBackDirectionParamHash = Animator.StringToHash("KnockBackDirection");
			m_knockBackAnimSpeedParamHash = Animator.StringToHash("KnockBackAnimSpeed");
			
			m_mainAttackStartTriggerHash = Animator.StringToHash("MainAttackStart");
			m_mainAttackFinishTriggerHash = Animator.StringToHash("MainAttackFinish");
			
			m_sideMainAttackStartTriggerHash = Animator.StringToHash("SideMainAttackStart");
			m_sideMainAttackFinishTriggerHash = Animator.StringToHash("SideMainAttackFinish");
			
			m_upMainAttackStartTriggerHash = Animator.StringToHash("UpMainAttackStart");
			m_upMainAttackFinishTriggerHash = Animator.StringToHash("UpMainAttackFinish");
			
			m_specialAttackStartTriggerHash = Animator.StringToHash("SpecialAttackStart");
			m_specialAttackFinishTriggerHash = Animator.StringToHash("SpecialAttackFinish");
			
			m_upSpecialAttackStartTriggerHash = Animator.StringToHash("UpSpecialAttackStart");
			m_upSpecialAttackFinishTriggerHash = Animator.StringToHash("UpSpecialAttackFinish");
			
			if (!m_animator)
			{
				throw new ArgumentNullException(nameof(m_animator));
			}
		}

		public void SetOnGround()
		{
			m_animator.SetBool(m_isFallingBoolHash, false);
			m_animator.SetBool(m_groundEntryBoolHash, true);
			m_animator.SetBool(m_isRisingBoolHash, false);
		}

		public void SetIdle()
		{
			m_animator.SetBool(m_isMovingBoolHash, false);
		}

		public void SetRunning()
		{
			m_animator.SetBool(m_isMovingBoolHash, true);
		}

		public void SetDashing()
		{
		}

		public void SetFalling()
		{
			m_animator.SetBool(m_groundEntryBoolHash, false);
			m_animator.SetBool(m_isFallingBoolHash, true);
			m_animator.SetBool(m_isRisingBoolHash, false);
		}

		public void SetJumping()
		{
			m_animator.SetBool(m_groundEntryBoolHash, false);
			m_animator.SetBool(m_isFallingBoolHash, false);
			m_animator.SetBool(m_isRisingBoolHash, true);
		}

		public void SetOnLedge()
		{
			m_animator.SetBool(m_groundEntryBoolHash, false);
			m_animator.SetBool(m_isFallingBoolHash, false);
			m_animator.SetTrigger(m_ledgeGrabTriggerHash);
		}

		public void SetClimbing()
		{
			m_animator.SetTrigger(m_climbTriggerHash);
			m_animator.SetBool(m_isFallingBoolHash, false);
			m_animator.SetBool(m_isRisingBoolHash, false);
		}
		
		public void SetKnockBackParameters(float direction, float animSpeed)
		{
			m_animator.SetFloat(m_knockBackDirectionParamHash, direction);
			m_animator.SetFloat(m_knockBackAnimSpeedParamHash, animSpeed);
		}
		
		public void SetKnockBack()
		{
			m_animator.SetTrigger(m_knockBackTriggerHash);
		}
		
		public void SetTossUp()
		{
		}
		
		public void SetWallSliding()
		{
		}

		#region Attack State Setters

		public void SetMainAttackWindUp()
		{
			m_animator.SetTrigger(m_mainAttackStartTriggerHash);
		}
		
		public void SetMainAttackFinish()
		{
			m_animator.SetTrigger(m_mainAttackFinishTriggerHash);
		}
		
		public void SetSideMainAttackWindUp()
		{
			m_animator.SetTrigger(m_sideMainAttackStartTriggerHash);
		}
		
		public void SetSideMainAttackFinish()
		{
			m_animator.SetTrigger(m_sideMainAttackFinishTriggerHash);
		}
		
		public void SetUpMainAttackWindUp()
		{
			m_animator.SetTrigger(m_upMainAttackStartTriggerHash);
		}
		
		public void SetUpMainAttackFinish()
		{
			m_animator.SetTrigger(m_upMainAttackFinishTriggerHash);
		}

		public void SetSpecialAttackWindUp()
		{
			m_animator.SetTrigger(m_specialAttackStartTriggerHash);
		}

		public void SetSpecialAttackFinish()
		{
			m_animator.SetTrigger(m_specialAttackFinishTriggerHash);
		}

		public void SetUpSpecialAttackWindUp()
		{
			m_animator.SetTrigger(m_upSpecialAttackStartTriggerHash);
		}
		

		public void SetUpSpecialAttackFinish()
		{
			m_animator.SetTrigger(m_upSpecialAttackFinishTriggerHash);
		}

		#endregion Attack State Setters
	}

	[Serializable]
	public class GraphicsProperty
	{
		[SerializeField] private string m_name;
		[SerializeField] private string m_animationName;
		[SerializeField] private float m_transitionDuration = 0.1f;
		[SerializeField] private GraphicState m_state;
		[SerializeField] private AudioClip m_clip;
		[SerializeField] private ParticleSystem m_particleSystem;
		[SerializeField] private bool m_loopingSound;
		[SerializeField] private bool m_loopingParticles;

		private int? AnimationHash { get; set; }
		public GraphicState State => m_state;

		public void SetAnimationHash()
		{
			if (string.IsNullOrEmpty(m_animationName))
			{
				AnimationHash = null;
				return;
			}
			AnimationHash = Animator.StringToHash(m_animationName);
		}

		public void PlayAnimation(ref Animator animator)
		{
			if (AnimationHash is null) return;
			animator.CrossFadeInFixedTime(AnimationHash.Value, m_transitionDuration);
		}
		
		public void PlaySound(ref AudioSource audioSource)
		{
			if (m_clip == null) return;
			if (m_loopingSound)
			{
				audioSource.resource = m_clip;
				audioSource.loop = true;
				audioSource.Play();
			}
			else audioSource.PlayOneShot(m_clip);
		}

		public void PlayParticle()
		{
			if (m_particleSystem == null) return;
			m_particleSystem.Play();
		}


		public void StopParticle()
		{
			if (!m_loopingParticles) return;
			m_particleSystem?.Stop();
		}
		
		public void StopSound(ref AudioSource audioSource)
		{
			if (!m_loopingSound) return;
			audioSource.Stop();
		}
		
	} 
}
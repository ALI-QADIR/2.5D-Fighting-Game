using System;
using System.Collections.Generic;
using Smash.StructsAndEnums;
using UnityEngine;

namespace Smash.Player
{
	public class PlayerGraphicsController : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private Animator m_animator;
		[SerializeField] private AudioSource m_audioSource;
		
		[Header("GraphicsData")]
		[SerializeField] private List<GraphicsProperty> m_graphicsProperties = new();

		private int m_jumps;
		
		private GraphicsProperty m_currentGraphicsProperty;
		
		private int m_idleAnimationIndex;
		private int m_landAnimationIndex;
		private int m_runningAnimationIndex;
		private int m_dashAnimationIndex;
		private int m_fallingAnimationIndex;
		private int m_ledgeAnimationIndex;
		private int m_jumpingAnimationIndex;
		private int m_flipAnimationIndex;
		private int m_climbAnimationIndex;
		private int m_wallSlideAnimationIndex;

		private void Awake()
		{
			if (m_animator == null)
			{
				throw new ArgumentNullException(nameof(m_animator));
			}

			if (m_audioSource == null)
			{
				throw new ArgumentNullException(nameof(m_audioSource));
			}

			foreach (var property in m_graphicsProperties)
			{
				property.SetAnimationHash();
			}
			
			m_idleAnimationIndex = GetIndexByState(GraphicState.Idle);
			m_landAnimationIndex = GetIndexByState(GraphicState.Land);
			m_runningAnimationIndex = GetIndexByState(GraphicState.Running);
			m_dashAnimationIndex = GetIndexByState(GraphicState.Dash);
			m_fallingAnimationIndex = GetIndexByState(GraphicState.Fall);
			m_ledgeAnimationIndex = GetIndexByState(GraphicState.Ledge);
			m_jumpingAnimationIndex = GetIndexByState(GraphicState.Jump);
			m_flipAnimationIndex = GetIndexByState(GraphicState.Flip);
			m_climbAnimationIndex = GetIndexByState(GraphicState.Climb);
			m_wallSlideAnimationIndex = GetIndexByState(GraphicState.WallSlide);
			
			m_currentGraphicsProperty = m_graphicsProperties[m_idleAnimationIndex];
		}
		
		private int GetIndexByState(GraphicState state) => m_graphicsProperties.FindIndex(x => x.State == state);

		public void SetOnGround()
		{
			m_jumps = 0;
			if (m_landAnimationIndex == -1) return;
			
			PlayGraphicAtIndex(m_landAnimationIndex);
		}

		public void SetIdle()
		{
			if (m_idleAnimationIndex == -1) return;
			
			PlayGraphicAtIndex(m_idleAnimationIndex);
		}

		public void SetRunning()
		{
			if (m_runningAnimationIndex == -1) return;
			
			PlayGraphicAtIndex(m_runningAnimationIndex);
		}

		public void SetDashing()
		{
			if (m_dashAnimationIndex == -1) return;
			
			PlayGraphicAtIndex(m_dashAnimationIndex);
		}

		public void SetFalling()
		{
			if (m_fallingAnimationIndex == -1) return;
			
			PlayGraphicAtIndex(m_fallingAnimationIndex);
		}

		public void SetOnLedge()
		{
			m_jumps = 1;
			if (m_ledgeAnimationIndex == -1) return;
			
			PlayGraphicAtIndex(m_ledgeAnimationIndex);
		}

		public void SetJumping()
		{
			m_jumps++;
			
			if (m_jumpingAnimationIndex == -1) return;
			var index = m_jumpingAnimationIndex;
			
			if (m_jumps > 1)
			{
				if (m_flipAnimationIndex != -1)
					index = m_flipAnimationIndex;
			}
			PlayGraphicAtIndex(index);
		}

		public void SetClimbing()
		{
			if (m_climbAnimationIndex == -1) return;
			
			PlayGraphicAtIndex(m_climbAnimationIndex);
		}
		
		public void SetWallSliding()
		{
			if (m_wallSlideAnimationIndex == -1) return;
			
			PlayGraphicAtIndex(m_wallSlideAnimationIndex);
		}

		private void PlayGraphicAtIndex(int index)
		{
			m_currentGraphicsProperty.StopParticle();
			m_currentGraphicsProperty.StopSound(ref m_audioSource);
			
			m_currentGraphicsProperty = m_graphicsProperties[index];
			
			m_currentGraphicsProperty.PlayAnimation(ref m_animator);
			m_currentGraphicsProperty.PlaySound(ref m_audioSource);
			m_currentGraphicsProperty.PlayParticle();
		}
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
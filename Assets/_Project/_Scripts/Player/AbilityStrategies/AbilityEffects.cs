using System;
using JetBrains.Annotations;
using Smash.Player.Components;
using TripleA.Utils.Extensions;
using UnityEngine;

namespace Smash.Player.AbilityStrategies
{
	[Serializable]
	public abstract class AbilityEffect
	{
		[field: SerializeField, InspectorReadOnly] public bool IsSelfEffect { get; protected set; }
		public float HeldDuration { protected get; set; }
		protected float _modifier;
		
		public abstract void Execute(Collider collider, Collider effectOwner);
		protected virtual void ModifyEffect(){}
	}
	

	[Serializable]
	public class DamageEffect : AbilityEffect
	{
		[SerializeField] private int m_maxDamage = 10;
		[SerializeField] private AnimationCurve m_damageCurve;

		public override void Execute(Collider collider, Collider effectOwner)
		{
			ModifyEffect();
			if (collider.TryGetComponent<CharacterHealth>(out var health))
				health.TakeDamage(m_maxDamage * _modifier);
		}

		protected override void ModifyEffect()
		{
			_modifier = m_damageCurve.Evaluate(HeldDuration / 3f); // TODO: remove magic numbers
		}
	}
	
	[Serializable]
	public class JumpEffect : AbilityEffect
	{
		[SerializeField] private float m_maxJumpForce;

		public JumpEffect()
		{
			IsSelfEffect = true;
		}
		
		public override void Execute(Collider collider, Collider effectOwner)
		{
			ModifyEffect();
			if (collider.TryGetComponent<CharacterPawn>(out var pawn))
			{
				pawn.HandleJumpAbility(m_maxJumpForce * _modifier);
			}
		}

		protected override void ModifyEffect()
		{
			_modifier = Mathf.Lerp(0.5f, 1, HeldDuration / 3f); // TODO: remove magic numbers
		}
	}

	[Serializable]
	public class KnockBackEffect : AbilityEffect
	{
		[SerializeField] private float m_force;

		public override void Execute(Collider collider, Collider effectOwner)
		{
			CalculateDirection(collider.transform.position, effectOwner.transform.position, out var directionX);
			ModifyEffect();
			if (collider.TryGetComponent(out CharacterPawn pawn))
			{
				pawn.HandleKnockBack(m_force * _modifier, directionX, _modifier);
			}
			else if (collider.TryGetComponent<Rigidbody>(out var rb))
			{
				var direction = directionX > 0 ? Vector3.right : Vector3.left;
				rb.AddForce(direction * m_force, ForceMode.Impulse);
			}
		}

		private void CalculateDirection(Vector3 effectedColliderPos, Vector3 effectOwnerPos, out float directionX)
		{
			directionX = (effectedColliderPos - effectOwnerPos).x;
			directionX = directionX > 0 ? 1 : -1;
		}

		protected override void ModifyEffect()
		{
			_modifier = Mathf.Lerp(0.5f, 1, HeldDuration / 3f);
		}
	}
	
	public class TossUpEffect : AbilityEffect
	{
		[SerializeField] private float m_force;
		
		public override void Execute(Collider collider, Collider effectOwner)
		{
			ModifyEffect();
			if (collider.TryGetComponent(out CharacterPawn pawn))
			{
				pawn.HandleTossUpAbility(m_force);
			}
			else if (collider.TryGetComponent(out Rigidbody rb))
			{
				rb.AddForce(Vector3.up * m_force, ForceMode.Impulse);
			}
		}
	}

	[Serializable]
	public class StunEffect : AbilityEffect
	{
		[SerializeField] private float m_stunDuration;
		
		public override void Execute(Collider collider, Collider effectOwner)
		{
			Debug.Log("Stun");
		}
	}

	[Serializable]
	public class SlowEffect : AbilityEffect
	{
		[SerializeField] private float m_slowDuration;
		[SerializeField, Range(0, 1)] private float m_slowMultiplier;
		
		public override void Execute(Collider collider, Collider effectOwner)
		{
			Debug.Log("Slow");
		}
	}

	[Serializable]
	public class NoEffect : AbilityEffect
	{
		public override void Execute(Collider collider, Collider effectOwner)
		{
			Debug.Log("No effect");
		}
	}
}

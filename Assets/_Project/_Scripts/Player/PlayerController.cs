using System;
using Smash.Player.Input;
using TripleA.Extensions;
using TripleA.FSM;
using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(PlayerMotor))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private InputReaderSO m_input;
		[SerializeField] private PlayerMotor m_motor;
		[SerializeField] private PlayerPropertiesSO m_properties;

		public Vector3 Direction { get; set; }

		private Transform m_tr;
		private StateMachine m_stateMachine;

		private float m_speed;
		private float m_airControlRate;
		private float m_gravity;
		private float m_maxFallSpeed;
		private float m_acceleration;
		private float m_airAcceleration;
		private bool m_useLocalVelocity;
		private Vector3 m_velocity, m_savedVelocity, m_savedInputVelocity;

		private void Awake()
		{
			m_tr = transform;
			m_motor = GetComponent<PlayerMotor>();

			m_speed = m_properties.MovementSpeed;
			m_airControlRate = m_properties.AirControlRate;
			m_gravity = m_properties.Gravity;
			m_maxFallSpeed = m_properties.MaxFallSpeed;
			m_acceleration = m_properties.GroundAcceleration;
			m_airAcceleration = m_properties.AirAcceleration;
		}

		private void Start()
		{
			m_input.EnablePlayerActions();
		}

		private void FixedUpdate()
		{
			// Todo: snap to ground on slopes??
			// Todo: jump
			// Todo: slope slide when above slope limit
			m_motor.CheckForGround();

			HandleVelocity();
			
			m_motor.SetVelocity(m_savedVelocity);
			/*HandleVelocity();
			m_motor.CheckForGround();
			Vector3 velocity = m_motor.IsGrounded() ? CalculateMovementVelocity() : Vector3.zero;
			
			velocity += m_useLocalVelocity ? m_tr.localToWorldMatrix * m_velocity : m_velocity;
			
			m_motor.SetExtendedSensor(m_motor.IsGrounded());
			m_motor.SetVelocity(velocity);

			m_savedVelocity = velocity;
			m_savedInputVelocity = CalculateMovementVelocity();*/
		}

		private void HandleVelocity()
		{
			Vector3 horizontalVelocity = Vector3Math.ExtractDotVector(m_savedVelocity, m_tr.right);
			Vector3 verticalVelocity = m_savedVelocity - horizontalVelocity;
			
			AdjustHorizontalVelocity(ref horizontalVelocity);
			AdjustVerticalVelocity(ref verticalVelocity);
			
			m_savedVelocity = horizontalVelocity + verticalVelocity;
		}

		private void AdjustVerticalVelocity(ref Vector3 verticalVelocity)
		{
			verticalVelocity = Vector3.MoveTowards(verticalVelocity, m_tr.up * -m_maxFallSpeed,
				m_gravity * Time.fixedDeltaTime);
			
			if (m_motor.IsGrounded() && Vector3Math.GetDotProduct(verticalVelocity, m_tr.up) < 0f)
			{
				verticalVelocity = Vector3.zero;
			}
		}

		private void AdjustHorizontalVelocity(ref Vector3 horizontalVelocity)
		{
			float acceleration = m_motor.IsGrounded() ? m_acceleration : m_airAcceleration;
			
			m_velocity = GetMovementVelocity();

			horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, m_velocity, 
				acceleration * Time.fixedDeltaTime);
		}

		private Vector3 GetMovementVelocity()
		{
			return Direction * m_speed;
		}
		

		/*private void HandleVelocity()
		{
			if (m_useLocalVelocity) m_velocity = m_tr.localToWorldMatrix * m_velocity;
			
			Vector3 verticalVelocity = Vector3Math.ExtractDotVector(m_velocity, m_tr.up);
			Vector3 horizontalVelocity = m_velocity - verticalVelocity;
			
			verticalVelocity -= m_tr.up * (m_gravity * Time.fixedDeltaTime);
			
			if (verticalVelocity.y < -m_maxFallSpeed) verticalVelocity.y = -m_maxFallSpeed;

			if (m_motor.IsGrounded() && Vector3Math.GetDotProduct(verticalVelocity, m_tr.up) < 0f)
			{
				verticalVelocity = Vector3.zero;
			}

			if (!m_motor.IsGrounded())
			{
				AdjustHorizontalVelocity(ref horizontalVelocity, CalculateMovementVelocity());
			}
			
			horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, Time.fixedDeltaTime);
			
			m_velocity = horizontalVelocity + verticalVelocity;
			
			if (m_useLocalVelocity) m_velocity = m_tr.worldToLocalMatrix * m_velocity;
		}

		private void AdjustHorizontalVelocity(ref Vector3 horizontalVelocity, Vector3 movementVelocity)
		{
			if (horizontalVelocity.magnitude > m_speed)
			{
				if (Vector3Math.GetDotProduct(movementVelocity, horizontalVelocity.normalized) > 0f)
				{
					movementVelocity = Vector3Math.RemoveDotVector(movementVelocity, horizontalVelocity.normalized);
					horizontalVelocity += movementVelocity * (Time.fixedDeltaTime * m_airControlRate * 0.25f);
				}
				else
				{
					horizontalVelocity += movementVelocity * (Time.fixedDeltaTime * m_airControlRate);
					horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, m_speed);
				}
			}
		}
		
		public Vector3 GetVelocity() => m_useLocalVelocity ? m_tr.localToWorldMatrix * m_velocity : m_velocity;

		private Vector3 CalculateMovementVelocity() => m_tr.right * (m_input.Direction.x * m_speed);*/
	}
}
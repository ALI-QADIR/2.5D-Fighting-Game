using TripleA.Extensions;
using TripleA.FSM;
using TripleA.ImprovedTimer.Timers;
using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(PlayerMotor))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private PlayerMotor m_motor;
		[SerializeField] private PlayerPropertiesSO m_properties;

		public Vector3 Direction { get; set; }

		private Transform m_tr;
		private StateMachine m_stateMachine;

		private float m_speed;
		private float m_jumpPower;
		private float m_gravity;
		private float m_maxFallSpeed;
		private float m_acceleration;
		private int m_numberOfJumps;
		private bool m_useLocalVelocity;
		private bool m_isJumping;
		private Vector3 m_velocity, m_savedVelocity;

		private CountDownTimer m_jumpTimer;

		#region Unity Methods

		private void Awake()
		{
			m_tr = transform;
			m_motor ??= GetComponent<PlayerMotor>();
			m_jumpTimer = new CountDownTimer(1f);

			m_speed = m_properties.GroundSpeed;
			m_gravity = m_properties.AirGravity;
			m_maxFallSpeed = m_properties.MaxFallSpeed;
			m_acceleration = m_properties.GroundAcceleration;
			m_numberOfJumps = m_properties.NumberOfJumps;
			m_jumpPower = m_properties.JumpPower;
			
			// SetUpStateMachine();
		}

		private void OnEnable()
		{
			m_jumpTimer.onTimerEnd += JumpEnded;
		}

		private void OnDisable()
		{
			m_jumpTimer.onTimerEnd -= JumpEnded;
		}

		private void FixedUpdate()
		{
			// m_stateMachine.OnFixedUpdate();
			// Todo: snap to ground on slopes??
			// Todo: jump
			// Todo: slope slide when above slope limit
			m_motor.CheckForGround();
			
			// handle using states
			if (m_motor.IsGrounded()) SetOnGround();
			else SetInAir();

			m_motor.SetExtendedSensor(m_motor.IsGrounded());
			
			HandleVelocity();
			// HandleJump();
			
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

		#endregion Unity Methods

		#region Public Methods

		public void HandleJumpInput()
		{
			// if (m_numberOfJumps <= 0) return;
			if (m_isJumping) return;
			m_numberOfJumps--;
			m_isJumping = true;
			m_jumpTimer.Start();
			HandleJump();
			Debug.Log($"Jumping {m_numberOfJumps}");
		}

		public void SetInAir()
		{
			m_speed = m_properties.AirSpeed;
			m_gravity = m_properties.AirGravity;
			m_acceleration = m_properties.AirAcceleration;
			m_maxFallSpeed = m_properties.MaxFallSpeed;
		}

		public void SetOnGround()
		{
			m_speed = m_properties.GroundSpeed;
			m_gravity = m_properties.GroundGravity;
			m_acceleration = m_properties.GroundAcceleration;
			m_maxFallSpeed = 0f;
		}

		#endregion Public Methods

		#region Private Methods

		private void JumpEnded()
		{
			Debug.Log("Jump ended");
			m_jumpTimer.Reset();
			m_isJumping = false;
		}

		private void HandleJump()
		{
			if (!m_isJumping) return;
			Vector3 verticalVelocity = m_tr.up * m_jumpPower;
			m_savedVelocity = Vector3Math.RemoveDotVector(m_savedVelocity, m_tr.up);
			m_savedVelocity += verticalVelocity;
			Debug.Log(m_savedVelocity);
		}

		private void HandleVelocity()
		{
			Vector3 horizontalVelocity = Vector3Math.ExtractDotVector(m_savedVelocity, m_tr.right);
			Vector3 verticalVelocity = Vector3Math.ExtractDotVector(m_savedVelocity, m_tr.up);
			
			AdjustHorizontalVelocity(ref horizontalVelocity);
			AdjustVerticalVelocity(ref verticalVelocity);
			
			m_savedVelocity = horizontalVelocity + verticalVelocity;
		}

		private void AdjustVerticalVelocity(ref Vector3 verticalVelocity)
		{
			verticalVelocity = Vector3.MoveTowards(verticalVelocity, m_tr.up * -m_maxFallSpeed,
				m_gravity * Time.fixedDeltaTime);
			
			// if (m_stateMachine.CurrentState is PlayerIdleGrounded && Vector3Math.GetDotProduct(verticalVelocity, m_tr.up) < 0f)
			/*if (m_motor.IsGrounded() )
			{
				verticalVelocity = Vector3.zero;
			}*/
		}

		private void AdjustHorizontalVelocity(ref Vector3 horizontalVelocity)
		{
			m_velocity = GetMovementVelocity();

			horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, m_velocity, 
				m_acceleration * Time.fixedDeltaTime);
		}

		private Vector3 GetMovementVelocity()
		{
			return Direction * m_speed;
		}

		private void SetUpStateMachine()
		{
			m_stateMachine = new StateMachine();
		}

		private void AddTransition(IState from, IState to, IPredicate condition) =>
			m_stateMachine.AddTransition(from, to, condition);
		
		private void AddAnyTransition(IState to, IPredicate condition) => m_stateMachine.AddAnyTransition(to, condition);
		
		private bool IsRising() => Vector3Math.GetDotProduct(m_savedVelocity, m_tr.up) > 0f && !m_motor.IsGrounded();
		private bool IsFalling() => Vector3Math.GetDotProduct(m_savedVelocity, m_tr.up) < 0f && !m_motor.IsGrounded();
		// private bool IsGrounded() => m_stateMachine.CurrentState is PlayerIdleGrounded or PlayerSlipping;
		private bool IsGroundTooSteep() => m_motor.IsGroundTooSteep();
		
		#endregion Private Methods


		/* old code for reference
		private void HandleVelocity()
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
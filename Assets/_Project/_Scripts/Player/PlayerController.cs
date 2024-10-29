using Smash.Player.States;
using TripleA.Extensions;
using TripleA.FSM;
using TripleA.ImprovedTimer.Timers;
using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(PlayerMotor))]
	public class PlayerController : MonoBehaviour
	{
		#region Fields

		[SerializeField] private PlayerMotor m_motor;
		[SerializeField] private PlayerPropertiesSO m_properties;
		[SerializeField] private float m_jumpBufferTime = 0.1f;
		[SerializeField] private float m_coyoteTime = 0.1f;

		private float m_speed;
		private float m_jumpPower;
		private float m_gravity;
		private float m_maxFallSpeed;
		private float m_acceleration;
		private int m_numberOfJumps;
		private int m_numberOfDashes;
		private bool m_isJumping;
		private bool m_isDashing;
		private bool m_canCoyote;
		private Vector3 m_velocity, m_savedVelocity;

		private Transform m_tr;
		private StateMachine m_stateMachine;
		private CountDownTimer m_jumpBufferTimer;

		private GroundedSubStateMachine m_groundedState;
		private AirborneSubStateMachine m_airborneState;
		private PlayerInit m_initState;

		#endregion Fields

		#region Properties

		public float CoyoteTime => m_coyoteTime;
		public Vector3 Direction { get; set; }

		#endregion
		
		#region Unity Methods

		private void Awake()
		{
			m_tr = transform;
			m_motor ??= GetComponent<PlayerMotor>();

			m_speed = m_properties.GroundSpeed;
			m_gravity = m_properties.AirGravity;
			m_maxFallSpeed = m_properties.MaxFallSpeed;
			m_acceleration = m_properties.GroundAcceleration;
			m_numberOfJumps = m_properties.NumberOfJumps;
			m_jumpPower = m_properties.JumpPower;
			m_numberOfDashes = m_properties.NumberOfDashes;

			m_jumpBufferTimer = new CountDownTimer(m_jumpBufferTime);
			
			SetUpStateMachine();
		}

		private void Update()
		{
			m_stateMachine.OnUpdate();
		}

		private void FixedUpdate()
		{
			m_stateMachine.OnFixedUpdate();
			// Todo: Ledge Grab
			// Todo: Dash
			// Todo: Wall Jump
			// Todo: Slopes??
			// Todo: Wall Clipping
			m_motor.CheckForGround();

			m_motor.SetExtendedSensor(m_motor.IsGrounded());
			
			HandleVelocity();
			
			m_motor.SetVelocity(m_savedVelocity);
		}

		#endregion Unity Methods

		#region Public Methods

		public void HandleJumpInput()
		{
			if (m_numberOfJumps <= 0)
			{
				if (m_jumpBufferTimer.IsRunning) m_jumpBufferTimer.Reset();	
				m_jumpBufferTimer.Start();
				return;
			}
			if (!m_canCoyote && !m_isJumping && m_stateMachine.CurrentState is AirborneSubStateMachine) 
				m_numberOfJumps--;
			
			m_numberOfJumps--;
			m_isJumping = true;
			HandleJump();
			// Debug.Log($"Jumping {m_numberOfJumps}");
		}

		public void SetInAir()
		{
			m_speed = m_properties.AirSpeed;
			m_numberOfDashes = m_properties.NumberOfDashes;
			m_gravity = m_properties.AirGravity;
			m_acceleration = m_properties.AirAcceleration;
			m_maxFallSpeed = m_properties.MaxFallSpeed;
		}

		public void SetOnGround()
		{
			m_numberOfJumps = m_properties.NumberOfJumps;
			m_numberOfDashes = m_properties.NumberOfDashes;
			m_speed = m_properties.GroundSpeed;
			m_gravity = m_properties.GroundGravity;
			m_acceleration = m_properties.GroundAcceleration;
			m_maxFallSpeed = 0f;
			RemoveVerticalVelocity();
			m_canCoyote = false;
			m_isJumping = false;
			
			if (!m_jumpBufferTimer.IsRunning) return;
			m_jumpBufferTimer.Reset();
			HandleJumpInput();
		}

		public void SetCoyote(bool canCoyote)
		{
			m_canCoyote = canCoyote;
		}
		
		public bool IsRising() => 
			Vector3Math.GetDotProduct(m_savedVelocity, m_tr.up) > 0f && !m_motor.IsGrounded();
		public bool IsFalling() => 
			Vector3Math.GetDotProduct(m_savedVelocity, m_tr.up) < 0f && !m_motor.IsGrounded();
		public bool IsMoving() => 
			Vector3Math.RemoveDotVector(m_savedVelocity, m_tr.up).sqrMagnitude > 0.01f && m_motor.IsGrounded(); 
		public bool IsGroundTooSteep() => m_motor.IsGroundTooSteep();

		#endregion Public Methods

		#region Private Methods

		private void HandleJump()
		{
			Vector3 verticalVelocity = m_tr.up * m_jumpPower;
			m_savedVelocity = Vector3Math.RemoveDotVector(m_savedVelocity, m_tr.up);
			m_savedVelocity += verticalVelocity;
			// Debug.Log(m_savedVelocity);
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

		private void RemoveVerticalVelocity()
		{
			m_savedVelocity = Vector3Math.RemoveDotVector(m_savedVelocity, m_tr.up);
		}

		private void SetUpStateMachine()
		{
			m_stateMachine = new StateMachine();

			m_groundedState = new GroundedSubStateMachine(this);
			m_airborneState = new AirborneSubStateMachine(this);
			m_initState = new PlayerInit();
			
			AddTransition(m_initState, m_groundedState, 
				new FuncPredicate(() => m_stateMachine.CurrentState is PlayerInit && m_motor.IsGrounded()));
			AddTransition(m_initState, m_airborneState,
				new FuncPredicate(() => m_stateMachine.CurrentState is PlayerInit && !m_motor.IsGrounded()));
			AddTransition(m_groundedState, m_airborneState, 
				new FuncPredicate(() => m_stateMachine.CurrentState is GroundedSubStateMachine && !m_motor.IsGrounded()));
			AddTransition(m_airborneState, m_groundedState,
				new FuncPredicate(() => m_stateMachine.CurrentState is AirborneSubStateMachine && m_motor.IsGrounded()));
			
			m_stateMachine.SetState(m_initState);
		}

		private void AddTransition(IState from, IState to, IPredicate condition) =>
			m_stateMachine.AddTransition(from, to, condition);
		
		private void AddAnyTransition(IState to, IPredicate condition) => m_stateMachine.AddAnyTransition(to, condition);
		
		#endregion Private Methods
	}
}
using System;
using Smash.Player.States;
using TripleA.Extensions;
using TripleA.FSM;
using TripleA.ImprovedTimer.Timers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Smash.Player
{
	[RequireComponent(typeof(PlayerMotor))]
	public class PlayerController : MonoBehaviour
	{
		#region Fields

		[Header("References")]
		[SerializeField] private PlayerMotor m_motor;
		[SerializeField] private LedgeDetector m_ledgeDetector;
		[SerializeField] private PlayerPropertiesSO m_properties;
		[FormerlySerializedAs("m_rotationDuration")]
		[Header("Control Values")]
		[SerializeField] private float m_groundGravity = 200f;
		[SerializeField] private float m_airGravity = 200f;
		[SerializeField] private float m_maxFallSpeed = 50f;
		[SerializeField] private float m_climbUpSpeed = 37f;
		[SerializeField] private float m_climbSideSpeed = 20f;
		[Header("Timer Values")]
		[SerializeField] private float m_jumpBufferTime = 0.1f;
		[SerializeField] private float m_coyoteTime = 0.1f;
		[SerializeField] private float m_dashResetTime = 0.2f;
		[Header("Apex")]
		[SerializeField] private float m_apexTime = 0.05f;
		[SerializeField, Range(0,1)] private float m_apexSpeedBoostRatio = 0.05f;

		private float m_currentMoveSpeed;
		private float m_jumpPower;
		private float m_gravity;
		private float m_currentFallSpeed;
		private float m_acceleration;
		private float m_elapsedTime;
		private float m_lookCompletion;
		private int m_numberOfJumps;
		private int m_numberOfDashes;
		private bool m_isJumping;
		private bool m_isLaunching;
		private Vector3 m_velocity, m_savedVelocity;
		private Quaternion m_savedRotation, m_targetRotation;

		private FallType m_fallType;
		
		private Transform m_tr;
		private StateMachine m_stateMachine;
		private CountDownTimer m_jumpBufferTimer;
		private CountDownTimer m_dashResetTimer;

		private GroundedSubStateMachine m_groundedState;
		private AirborneSubStateMachine m_airborneState;
		private PlayerInit m_initState;

		#endregion Fields

		#region Properties
		
		public float CoyoteTime => m_coyoteTime;
		public float TimeToRotate => m_properties.TimeToRotate;
		public float DashDuration => m_properties.DashDuration; 
		public float ApexTime => m_apexTime; 
		public Vector3 Direction { get; set; }
		public IState CurrentState { get; set; }

		#endregion
		
		public event Action<bool> OnDash = delegate { }; 
		public event Action OnRotate = delegate { }; 
		
		#region Unity Methods

		private void Awake()
		{
			m_tr = transform;
			m_motor ??= GetComponent<PlayerMotor>();
			m_ledgeDetector ??= GetComponent<LedgeDetector>();

			m_currentMoveSpeed = m_properties.GroundSpeed;
			m_gravity = m_airGravity;
			m_currentFallSpeed = m_maxFallSpeed;
			m_acceleration = m_properties.GroundAcceleration;
			m_numberOfJumps = m_properties.NumberOfJumps;
			m_jumpPower = m_properties.JumpPower;
			m_numberOfDashes = m_properties.NumberOfDashes;

			m_jumpBufferTimer = new CountDownTimer(m_jumpBufferTime);
			m_dashResetTimer = new CountDownTimer(m_dashResetTime);

			m_dashResetTimer.onTimerEnd += () => m_numberOfDashes = m_properties.NumberOfDashes;
			
			SetUpStateMachine();
		}

		private void Update()
		{
			m_stateMachine.OnUpdate();
		}

		private void FixedUpdate()
		{
			m_stateMachine.OnFixedUpdate();
			// Todo: One-Way Platforms
			// Todo: Wall Jump
			// Todo: Slopes??
			// Todo: Stairs??
			// Todo: Wall Clipping
			CheckForLedge();
			m_motor.CheckForGround();

			m_motor.SetExtendedSensor(m_motor.IsGrounded());
			
			HandleVelocity();
			
			m_motor.SetVelocity(m_savedVelocity);
		}

		#endregion Unity Methods

		#region Public Methods

		#region Input

		public void HandleUpInput()
		{
			if (CurrentState is Ledge)
			{
				// Todo : Climb
				HandleClimb();
				return;
			}
			 // else if (CurrentState is WallSlide)
			 // {
				//  wall jump
				//  return;
			 // }
			HandleJumpInput();
		}

		public void HandleDownInput()
		{
			if (m_motor.IsGrounded())
			{
				// pass through platform
				return;
			}
			if (CurrentState is Falling or FloatingFall or Rising or Dash)
			{
				m_fallType = FallType.Crash;
				return;
			}
			if (CurrentState is Ledge)
			{
				m_fallType = FallType.Crash;
			}
		}

		public void Rotate(float angle)
		{
			if (angle == 0) return;
			OnRotate?.Invoke();
			float lookAngle = Mathf.Approximately(angle, -1.0f) ? 179f : 0f;
			m_targetRotation = Quaternion.Euler(0f, lookAngle, 0f);
			m_savedRotation = m_tr.rotation;
		}

		public void HandleJumpInput()
		{
			if (m_numberOfJumps <= 0)
			{
				if (m_jumpBufferTimer.IsRunning) m_jumpBufferTimer.Reset();	
				m_jumpBufferTimer.Start();
				return;
			}

			if (CurrentState is Dash)
			{
				if (m_jumpBufferTimer.IsRunning) m_jumpBufferTimer.Reset();	
				m_jumpBufferTimer.Start();
				return;
			}
			if (CurrentState is not Coyote && (!m_isJumping || !m_isLaunching) && m_stateMachine.CurrentState is AirborneSubStateMachine) 
				m_numberOfJumps--;
			
			m_numberOfJumps--;
			m_isJumping = true;
			HandleJump();
			// Debug.Log($"Jumping {m_numberOfJumps}");
		}

		public void HandleDashInput()
		{
			if (m_numberOfDashes <= 0 || Direction == Vector3.zero || CurrentState is CrashingFall) return;
			OnDash?.Invoke(true);
			if (CurrentState is Coyote) return;
			m_numberOfDashes--;
		}

		public void HandleLaunchInput()
		{
			if (m_isLaunching) return;
			m_isLaunching = true;
			m_numberOfJumps = 0;
			m_fallType = FallType.Normal;
			HandleLaunch();
		}
		
		public void HandleLaunchAndCrashInput()
		{
			if (m_isLaunching) return;
			m_isLaunching = true;
			m_numberOfJumps = 0;
			m_fallType = FallType.Crash;
			HandleLaunch();
		}

		public void HandleLaunchAndFloatInput()
		{
			if (m_isLaunching) return;
			m_isLaunching = true;
			m_numberOfJumps = 0;
			m_fallType = FallType.Float;
			HandleLaunch();
		}
		
		#endregion Input

		#region State Setters

		public void SetInAir()
		{
			m_currentMoveSpeed = m_isLaunching ? 0 : m_properties.AirSpeed;
			m_numberOfDashes = m_properties.NumberOfDashes;
			m_gravity = m_airGravity;
			m_acceleration = m_properties.AirAcceleration;
			m_currentFallSpeed = m_maxFallSpeed;
		}

		public void SetOnGround()
		{
			m_numberOfJumps = m_properties.NumberOfJumps;
			m_numberOfDashes = m_properties.NumberOfDashes;
			m_currentMoveSpeed = m_properties.GroundSpeed;
			m_gravity = m_groundGravity;
			m_acceleration = m_properties.GroundAcceleration;
			m_currentFallSpeed = 0f;
			RemoveVerticalVelocity();
			m_isJumping = false;
			m_isLaunching = false;
			m_fallType = FallType.None;
			if (m_tr.rotation != m_targetRotation) OnRotate?.Invoke();

			HandleJumpBuffer();
		}
		
		public void HandleRotation(float lookCompletion)
		{
			m_tr.rotation = Quaternion.Slerp(m_savedRotation, m_targetRotation, lookCompletion);
		}

		public void SetDashStart()
		{
			// Debug.Log("Dashing");
			RemoveVerticalVelocity();
			m_dashResetTimer.Reset();
			m_gravity = 0f;
			Vector3 velocity = Direction * m_properties.DashSpeed;
			m_savedVelocity = Vector3Math.RemoveDotVector(m_savedVelocity, Direction);
			m_savedVelocity += velocity;
		}

		public void SetDashEnd()
		{
			// Debug.Log("Dash Ended");
			if (m_motor.IsGrounded())
			{
				m_dashResetTimer.Start();
				m_gravity = m_groundGravity;
				m_currentMoveSpeed = m_properties.GroundSpeed;
			}
			else
			{
				m_gravity = m_airGravity;
				m_currentMoveSpeed = m_properties.AirSpeed;
			}

			HandleJumpBuffer();
		}

		public void SetApex(bool isApex)
		{
			if (isApex)
			{
				m_currentFallSpeed = 5f;
				m_currentMoveSpeed += m_apexSpeedBoostRatio * m_currentMoveSpeed;
			}
			else
			{
				m_currentFallSpeed = m_maxFallSpeed;
				m_currentMoveSpeed = m_properties.AirSpeed;
			}
		}

		public void SetFalling()
		{
			m_currentFallSpeed = m_maxFallSpeed;
			m_currentMoveSpeed = m_properties.AirSpeed;
		}

		public void SetFloatingFall()
		{
			m_currentFallSpeed = m_properties.FloatFallSpeed;
			m_currentMoveSpeed = m_properties.AirSpeed * m_properties.FloatControlRatio;
		}

		public void SetCrashingFall()
		{
			m_currentFallSpeed = m_properties.CrashSpeed;
			m_gravity = 1000f;
			m_currentMoveSpeed = 0f;
		}

		public void SetOnLedge(bool isLedge)
		{
			if (isLedge)
			{
				RemoveVerticalVelocity();
				m_fallType = FallType.Normal;
				m_ledgeDetector.SetOnLedge();
				m_currentFallSpeed = 0;
				m_currentMoveSpeed = 0;
				m_numberOfJumps = 1;
				m_ledgeDetector.ResetSensorHits();
			}
			else
			{
				m_currentFallSpeed = m_maxFallSpeed;
				m_numberOfDashes = 1;
			}
		}

		#endregion State Setters
		
		public bool IsRising() => 
			Vector3Math.GetDotProduct(m_savedVelocity, m_tr.up) > 0f && !m_motor.IsGrounded();
		public bool IsFalling() => 
			Vector3Math.GetDotProduct(m_savedVelocity, m_tr.up) < 0f && !m_motor.IsGrounded();
		public bool IsMoving() => 
			Vector3Math.GetDotProduct(m_savedVelocity, m_tr.right) != 0f && m_motor.IsGrounded(); 
		public bool IsNormalFall() => m_fallType is FallType.Normal or FallType.None;
		public bool IsFloatingFall() => m_fallType == FallType.Float;
		public bool IsCrashingFall() => m_fallType == FallType.Crash;
		public bool IsLedgeGrab() => m_ledgeDetector.IsLedgeDetected();
		public bool IsGroundTooSteep() => m_motor.IsGroundTooSteep();

		#endregion Public Methods

		#region Private Methods

		private void CheckForLedge()
		{
			if (CurrentState is Falling or FloatingFall or Apex or Coyote)
				m_ledgeDetector.CheckForLedge();
		}

		private void HandleJump()
		{
			m_jumpPower = m_properties.JumpPower;
			Vector3 verticalVelocity = m_tr.up * m_jumpPower;
			RemoveVerticalVelocity();
			m_savedVelocity += verticalVelocity;
			// Debug.Log(m_savedVelocity);
		}

		private void HandleClimb()
		{
			Vector3 verticalVelocity = m_tr.up * m_climbUpSpeed;
			Vector3 horizontalVelocity = m_tr.right * m_climbSideSpeed;
			RemoveVerticalVelocity();
			m_savedVelocity = horizontalVelocity + verticalVelocity;
		}

		private void HandleLaunch()
		{
			RemoveVerticalVelocity();
			m_jumpPower = m_properties.LaunchPower;
			m_isJumping = false;
			Vector3 verticalVelocity = m_tr.up * m_jumpPower;
			m_savedVelocity += verticalVelocity;
		}

		private void HandleJumpBuffer()
		{
			if (!m_jumpBufferTimer.IsRunning) return;
			m_jumpBufferTimer.Reset();
			HandleJumpInput();
		}

		private void HandleVelocity()
		{
			Vector3 verticalVelocity = Vector3Math.ExtractDotVector(m_savedVelocity, m_tr.up);
			Vector3 horizontalVelocity = Vector3Math.RemoveDotVector(m_savedVelocity, m_tr.up);
			
			AdjustHorizontalVelocity(ref horizontalVelocity);
			AdjustVerticalVelocity(ref verticalVelocity);
			
			m_savedVelocity = horizontalVelocity + verticalVelocity;
		}

		private void AdjustVerticalVelocity(ref Vector3 verticalVelocity)
		{
			verticalVelocity = Vector3.MoveTowards(verticalVelocity, m_tr.up * -m_currentFallSpeed,
				m_gravity * Time.fixedDeltaTime);
		}

		private void AdjustHorizontalVelocity(ref Vector3 horizontalVelocity)
		{
			if (CurrentState is Dash) return;
			
			m_velocity = GetMovementVelocity();
			horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, m_velocity, 
				m_acceleration * Time.fixedDeltaTime);
		}

		private Vector3 GetMovementVelocity()
		{
			return Direction * m_currentMoveSpeed;
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
			
			FuncPredicate groundToAirborne = new(() => 
				m_stateMachine.CurrentState is GroundedSubStateMachine && !m_motor.IsGrounded());
			FuncPredicate airborneToGround = new(() => 
				m_stateMachine.CurrentState is AirborneSubStateMachine && m_motor.IsGrounded());
			
			AddTransition(m_initState, m_groundedState, 
				new FuncPredicate(() => m_stateMachine.CurrentState is PlayerInit && m_motor.IsGrounded()));
			AddTransition(m_initState, m_airborneState,
				new FuncPredicate(() => m_stateMachine.CurrentState is PlayerInit && !m_motor.IsGrounded()));
			AddTransition(m_groundedState, m_airborneState, groundToAirborne);
			AddTransition(m_airborneState, m_groundedState, airborneToGround);
			
			m_stateMachine.SetState(m_initState);
		}

		private void AddTransition(IState from, IState to, IPredicate condition) =>
			m_stateMachine.AddTransition(from, to, condition);
		
		private void AddAnyTransition(IState to, IPredicate condition) => m_stateMachine.AddAnyTransition(to, condition);
		
		#endregion Private Methods
		private enum FallType { Normal, Crash, Float, None }
	}
	
}
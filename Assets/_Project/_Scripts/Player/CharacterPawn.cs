using System;
using Smash.Player.AttackStrategies;
using Smash.Player.Components;
using Smash.Player.States;
using TripleA.Utils.Extensions;
using TripleA.StateMachine.FSM;
using TripleA.Timers.ImprovedTimers;
using UnityEngine;

namespace Smash.Player
{
	[RequireComponent(typeof(PlayerMotor))]
	public class CharacterPawn : BasePawn
	{
		#region Fields
		
		[Header("References")]
		[SerializeField] private PlayerMotor m_motor;
		[SerializeField] private LedgeDetector m_ledgeDetector;
		[SerializeField] private CeilingDetector m_ceilingDetector;
		[SerializeField] private WallDetector m_wallDetector;
		[SerializeField] private PlayerGraphicsController m_graphicsController;
		[SerializeField] private CharacterPropertiesSO m_properties;
		
		[Header("Control Values")]
		[Space(10)]
		[SerializeField] private float m_groundGravity;
		[SerializeField] private float m_groundSpeed = 10f;
		[SerializeField] private float m_groundAcceleration = 200f;
		[Space(10)]
		[SerializeField] private float m_airGravity = 200f;
		[SerializeField] private float m_airSpeed = 10f;
		[SerializeField] private float m_airAcceleration = 100f;
		[SerializeField] private float m_maxFallSpeed = 40f;
		[Space(10)]
		[SerializeField] private float m_jumpPower = 40f;
		[SerializeField] private int m_maxNumberOfJumps = 2;
		[Space(10)]
		[SerializeField] private float m_wallSlideSpeed = 10f;
		[SerializeField] private float m_climbUpSpeed = 32f;
		[SerializeField] private float m_climbSideSpeed = 25f;
		// [SerializeField] private float m_wallJumpSideSpeed = 50f;
		[SerializeField] private float m_timeToRotate = 0.15f;
		
		[Header("Timer Values")]
		[SerializeField] private float m_jumpBufferTime = 0.1f;
		[SerializeField] private float m_coyoteTime = 0.1f;
		
		[Header("Apex")]
		[SerializeField] private float m_apexTime = 0.05f;
		[SerializeField, Range(0,1)] private float m_apexSpeedBoostRatio = 0.05f;
		
		private float m_currentMoveSpeed;
		private float m_gravity;
		private float m_currentFallSpeed;
		private float m_acceleration;
		private float m_elapsedTime;
		private float m_elapsedRotationTime;
		private float m_currentLookAngle;
		private int m_numberOfJumps;
		private bool m_isJumping;
		private bool m_isLaunching;
		private bool m_isClimbing;
		private Vector3 m_velocity, m_savedVelocity;
		private Quaternion m_savedRotation, m_targetRotation;
		
		private int m_targetLayers;
		private Transform m_tr;
		private StateMachine m_stateMachine;
		private CountDownTimer m_jumpBufferTimer;
		private CountDownTimer m_wallJumpBufferTimer;

		private GroundedSubStateMachine m_groundedState;
		private AirborneSubStateMachine m_airborneState;
		private PlayerInit m_initState;

		#endregion Fields

		#region Properties
		
		public float CoyoteTime => m_coyoteTime;
		public float ApexTime => m_apexTime; 
		public IState CurrentState { get; set; }
		public PlayerSubStateMachine CurrentStateMachine { get; set; }

		private Vector3 Direction { get; set; }

		#endregion Properties

		#region AttackStrategies

		[HideInInspector] public AttackStrategy mainAttackStrategy;
		[HideInInspector] public AttackStrategy sideMainAttackStrategy;
		[HideInInspector] public AttackStrategy upMainAttackStrategy;
		[HideInInspector] public AttackStrategy downMainAttackStrategy;
		[HideInInspector] public AttackStrategy specialAttackStrategy;
		[HideInInspector] public AttackStrategy sideSpecialAttackStrategy;
		[HideInInspector] public AttackStrategy upSpecialAttackStrategy;
		[HideInInspector] public AttackStrategy downSpecialAttackStrategy;

		#endregion AttackStrategies
		public event Action<bool> OnDash = delegate { };
		
		#region Unity Methods

		public override void Initialise()
		{
			// _inputHandler.SetCharacterPawn(this);
			
			SetUpComponents();

			SetUpVariables();

			SetUpTimers();

			SetUpGraphics();
			
			SetUpAttackStrategies();

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
			// Todo: Wall Clipping
			
			CheckForLedge();
			CheckWallSlide();
			CheckForCeiling();
			CheckRotation();
			HandleRotation();
			
			m_motor.CheckForGround();

			m_motor.SetExtendedSensor(m_motor.IsGrounded());
			
			HandleVelocity();
			
			m_motor.SetVelocity(m_savedVelocity);
		}

		#endregion Unity Methods

		#region Public Methods

		#region Input Handler

		public override void SetIndex(int index)
		{
			PlayerIndex = index;
		}

		public override void HandleRightInput() => Direction = Vector3.right;

		public override void HandleLeftInput() => Direction = Vector3.left;
		
		public override void HandleDpadNullInput() => Direction = Vector3.zero;

		public override void HandleUpInput()
		{
			if (CurrentState is Ledge)
			{
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

		public override void HandleDownInput()
		{
			// if (m_motor.IsGrounded())
			// {
			// pass through platform
			// return;
			// }
		}
		
		public override void HandleJumpInput()
		{
			if (m_numberOfJumps <= 0)
			{
				if (m_jumpBufferTimer.IsRunning) m_jumpBufferTimer.Reset();	
				m_jumpBufferTimer.Start();
				return;
			}

			if (CurrentState is MainAttackStart)
			{
				if (m_jumpBufferTimer.IsRunning) m_jumpBufferTimer.Reset();	
				m_jumpBufferTimer.Start();
				return;
			}
			if (CurrentState is not Coyote && (!m_isJumping || !m_isLaunching) && m_stateMachine.CurrentState is AirborneSubStateMachine) 
				m_numberOfJumps--;
			
			m_numberOfJumps--;
			m_isJumping = true;
			m_motor.ShouldAdjustForGround = false;
			/* // Wall jumping logic
			 if (CurrentState is WallSlide or Ledge || (CurrentState is Rising or Apex && IsWallDetected()) || m_wallJumpBufferTimer.IsRunning)
			{
				// TODO: Fix Z drift when wall Jumping => for now rigid body -> freeze z position
				float jumpDirectionMultiplier = m_wallJumpBufferTimer.IsRunning ? 0.8f : -1f;
				Vector3 horizontalVelocity = m_tr.right * (m_wallJumpSideSpeed * jumpDirectionMultiplier);
				m_savedVelocity += horizontalVelocity;
				float lookAngle = Mathf.Approximately(m_currentLookAngle, 179) ? 1 : -1;
				Rotate(lookAngle);
			}
			*/
			HandleJump();
		}

		public override void HandleMainAttackInputStart()
		{
			CurrentStateMachine.MainAttackHold = true;
		}

		public override void HandleMainAttackInputEnd(float heldTime)
		{
			CurrentStateMachine.MainAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			CurrentStateMachine.MainAttackHold = false;
		}
		
		public override void HandleSideMainAttackInputStart(int direction)
		{
			Rotate(direction);
			CurrentStateMachine.SideMainAttackHold = true;
		}
		
		public override void HandleSideMainAttackInputEnd(float heldTime, int direction)
		{
			Rotate(direction);
			CurrentStateMachine.SideMainAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			CurrentStateMachine.SideMainAttackHold = false;
		}
		
		public override void HandleUpMainAttackInputStart()
		{
			CurrentStateMachine.UpMainAttackHold = true;
		}
		
		public override void HandleUpMainAttackInputEnd(float heldTime)
		{
			CurrentStateMachine.UpMainAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			CurrentStateMachine.UpMainAttackHold = false;
		}

		public override void HandleDownMainAttackInputStart()
		{
			CurrentStateMachine.DownMainAttackHold = true;
		}

		public override void HandleDownMainAttackInputEnd(float heldTime)
		{
			CurrentStateMachine.DownMainAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			CurrentStateMachine.DownMainAttackHold = false;
		}

		public override void HandleSpecialAttackInputStart()
		{
			Debug.Log("Special Attack Input Start");
			CurrentStateMachine.SpecialAttackHold = true;
		}

		public override void HandleSpecialAttackInputEnd(float heldTime)
		{
			Debug.Log("Special Attack Input End");
			CurrentStateMachine.SpecialAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			CurrentStateMachine.SpecialAttackHold = false;
		}

		public override void HandleSideSpecialAttackInputStart(int direction)
		{
			Rotate(direction);
			CurrentStateMachine.SideSpecialAttackHold = true;
		}

		public override void HandleSideSpecialAttackInputEnd(float heldTime, int direction)
		{
			Rotate(direction);
			CurrentStateMachine.SideSpecialAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			CurrentStateMachine.SideSpecialAttackHold = false;
		}

		public override void HandleDownSpecialAttackInputStart()
		{
			CurrentStateMachine.DownSpecialAttackHold = true;
		}
		
		public override void HandleDownSpecialAttackInputEnd(float heldTime)
		{
			CurrentStateMachine.DownSpecialAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			CurrentStateMachine.DownSpecialAttackHold = false;
		}
		
		public override void HandleUpSpecialAttackInputStart()
		{
			CurrentStateMachine.UpSpecialAttackHold = true;
		}
		
		public override void HandleUpSpecialAttackInputEnd(float heldTime)
		{
			CurrentStateMachine.UpSpecialAttackTap = heldTime <= 0.2f; // TODO: remove magic number
			CurrentStateMachine.UpSpecialAttackHold = false;
		}
		
		#endregion Input Handler

		#region State Setters

		public void SetInAir()
		{
			m_currentMoveSpeed = m_isLaunching ? 0 : m_airSpeed;
			m_gravity = m_airGravity;
			m_acceleration = m_airAcceleration;
			m_currentFallSpeed = m_maxFallSpeed;
		}

		public void SetOnGround()
		{
			m_numberOfJumps = m_maxNumberOfJumps;
			m_currentMoveSpeed = m_groundSpeed;
			m_acceleration = m_groundAcceleration;
			m_gravity = m_groundGravity;
			
			m_currentFallSpeed = 0f;
			
			RemoveVerticalVelocity();
			m_motor.ShouldAdjustForGround = true;
			
			m_isJumping = false;
			m_isLaunching = false;

			HandleJumpBuffer();
		}

		public void SetMainAttackWindup()
		{
			Debug.Log("Main Attack Hold Start");
			RemoveVerticalVelocity();
			m_gravity = 0f;
		}

		public void SetMainAttackExecute()
		{
			Debug.Log("Main Attack End");
		}

		public void SetMainAttackFinish()
		{
			Debug.Log("Main Attack Finished");
			CurrentStateMachine.MainAttackHold = false;
			CurrentStateMachine.MainAttackTap = false;
			SetGravity();
		}

		public void SetSideMainAttackWindUp()
		{
			Debug.Log("Side Main Attack Windup");
		}
		
		public void SetSideMainAttackExecute()
		{
			Debug.Log("Side Main Attack Execute");
		}

		public void SetSideMainAttackFinish()
		{
			Debug.Log("Side Main Attack Finish");
			CurrentStateMachine.SideMainAttackHold = false;
			CurrentStateMachine.SideMainAttackTap = false;
			SetGravity();
		}
		
		public void SetUpMainAttackWindUp()
		{
			Debug.Log("Up Main Attack Windup");
		}
		
		public void SetUpMainAttackExecute()
		{
			Debug.Log("up Main Attack Execute");
		}

		public void SetUpMainAttackFinish()
		{
			Debug.Log("Up Main Attack Finish");
			CurrentStateMachine.UpMainAttackHold = false;
			CurrentStateMachine.UpMainAttackTap = false;
			SetGravity();
		}
		
		public void SetDownMainAttackWindUp()
		{
			Debug.Log("Down Main Attack Windup");
		}
		
		public void SetDownMainAttackExecute()
		{
			Debug.Log("Down Main Attack Execute");
		}

		public void SetDownMainAttackFinish()
		{
			Debug.Log("Down Main Attack Finish");
			CurrentStateMachine.DownMainAttackHold = false;
			CurrentStateMachine.DownMainAttackTap = false;
			SetGravity();
		}
		
		public void SetSpecialAttackWindup()
		{
			// Debug.Log("Dashing");
			Debug.Log("Special Attack Wind Up");
			// Play Attack Windup animation
			// m_graphicsController.SetDashing();
			RemoveVerticalVelocity();
			m_gravity = 0f;
		}

		// Call this when Special attack animation has to finish
		public void SetSpecialAttackExecute()
		{
			// Play attack execution animation
			Debug.Log("Special Attack End");
			// m_graphicsController.
		}

		public void SetSpecialAttackFinish()
		{
			// end attack execution animation
			Debug.Log("Special Attack Finished");
			CurrentStateMachine.SpecialAttackHold = false;
			CurrentStateMachine.SpecialAttackTap = false;
			// Debug.Log("Dash Ended");
			SetGravity();
			HandleJumpBuffer();
		}

		public void SetSideSpecialAttackWindUp()
		{
			Debug.Log("Side Special Attack Windup");
		}
		
		public void SetSideSpecialAttackExecute()
		{
			Debug.Log("Side Special Attack Execute");
		}

		public void SetSideSpecialAttackFinish()
		{
			Debug.Log("Side Special Attack Finish");
			CurrentStateMachine.SideSpecialAttackHold = false;
			CurrentStateMachine.SideSpecialAttackTap = false;
			SetGravity();
		}
		
		public void SetUpSpecialAttackWindUp()
		{
			Debug.Log("Up Special Attack Windup");
		}
		
		public void SetUpSpecialAttackExecute()
		{
			Debug.Log("up Special Attack Execute");
		}

		public void SetUpSpecialAttackFinish()
		{
			Debug.Log("Up Special Attack Finish");
			CurrentStateMachine.UpSpecialAttackHold = false;
			CurrentStateMachine.UpSpecialAttackTap = false;
			SetGravity();
		}
		
		public void SetDownSpecialAttackWindUp()
		{
			Debug.Log("Down Special Attack Windup");
		}
		
		public void SetDownSpecialAttackExecute()
		{
			Debug.Log("Down Special Attack Execute");
		}

		public void SetDownSpecialAttackFinish()
		{
			Debug.Log("Down Special Attack Finish");
			CurrentStateMachine.DownSpecialAttackHold = false;
			CurrentStateMachine.DownSpecialAttackTap = false;
			SetGravity();
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
				m_currentMoveSpeed = m_airSpeed;
			}
		}

		public void SetFalling()
		{
			m_currentFallSpeed = m_maxFallSpeed;
			m_currentMoveSpeed = m_airSpeed;
		}

		public void SetCrashingFall()
		{
			m_gravity = 1000f;
			m_currentMoveSpeed = 0f;
		}

		public void SetOnLedge(bool isLedge)
		{
			if (isLedge)
			{
				RemoveVerticalVelocity();
				m_ledgeDetector.SetOnLedge();
				m_currentFallSpeed = 0;
				m_currentMoveSpeed = 0;
				m_numberOfJumps = 1;
				m_ledgeDetector.ResetSensorHits();
				m_isJumping = false;
			}
			else
			{
				m_currentFallSpeed = m_maxFallSpeed;
			}
		}

		public void SetWallSliding(bool isWallSliding)
		{
			if (isWallSliding)
			{
				m_graphicsController.SetWallSliding();
				m_currentFallSpeed = m_wallSlideSpeed;
				m_numberOfJumps = 1;
				m_wallJumpBufferTimer.Reset();
				HandleJumpBuffer();
			}
			else
			{
				m_wallJumpBufferTimer.Start();
				m_currentFallSpeed = m_maxFallSpeed;
			}
		}

		#endregion State Setters
		
		public bool IsRising() => 
			Vector3Math.GetDotProduct(m_savedVelocity, m_tr.up) > 0f && !m_motor.IsGrounded();
		public bool IsFalling() => 
			Vector3Math.GetDotProduct(m_savedVelocity, m_tr.up) <= 0f && !m_motor.IsGrounded();
		public bool IsMoving() => 
			Vector3Math.GetDotProduct(m_savedVelocity, m_tr.right) != 0f && m_motor.IsGrounded();
		public bool IsLedgeGrab() => m_ledgeDetector.IsLedgeDetected();
		public bool IsWallDetected() => m_wallDetector.IsWallDetected();

		#endregion Public Methods

		#region Private Methods
		
		private void Rotate(int angle)
		{
			if (angle == 0) return;
			if (CurrentState is Ledge && !m_isJumping) return;
			
			float lookAngle = Mathf.Approximately(angle, -1.0f) ? 179f : 0f;
			if (Mathf.Approximately(m_currentLookAngle, lookAngle)) return;
			
			m_currentLookAngle = lookAngle;
			m_targetRotation = Quaternion.Euler(0f, lookAngle, 0f);
			m_savedRotation = m_tr.rotation;
			m_elapsedTime = 0;
		}

		private void HandleClimb()
		{
			m_isClimbing = true;
			Vector3 verticalVelocity = m_tr.up * m_climbUpSpeed;
			Vector3 horizontalVelocity = m_tr.right * m_climbSideSpeed;
			RemoveVerticalVelocity();
			m_savedVelocity = horizontalVelocity + verticalVelocity;
		}

		public bool IsClimbing()
		{
			bool temp = m_isClimbing;
			m_isClimbing = false;
			return temp;
		}
		
		private void CheckForLedge()
		{
			if (CurrentState is Falling or Apex or Coyote)
				m_ledgeDetector.CheckForLedge();
		}

		private void CheckWallSlide()
		{
			if (CurrentState is Ledge) return;
			if (CurrentState is Falling or WallSlide or Rising or Apex)
			{
				m_wallDetector.ResetSensorHits();
				m_wallDetector.CheckForWall();
			}
		}

		private void CheckForCeiling()
		{
			if (CurrentState is not Rising) return;
			
			m_ceilingDetector.ResetSensorHits();
			m_ceilingDetector.CheckForCeiling();
			
			if (!m_ceilingDetector.IsCeilingDetected()) return;
			
			Vector3 verticalVelocity =
				Vector3.Min(Vector3.zero, Vector3Math.ExtractDotVector(m_savedVelocity, m_tr.up));
			RemoveVerticalVelocity();
			m_savedVelocity += verticalVelocity;
		}

		private void SetGravity()
		{
			if (m_motor.IsGrounded())
			{
				m_gravity = m_groundGravity;
				m_currentMoveSpeed = m_groundSpeed;
			}
			else
			{
				m_gravity = m_airGravity;
				m_currentMoveSpeed = m_airSpeed;
			}
		}

		private void HandleRotation()
		{
			m_elapsedTime += Time.deltaTime;
			m_tr.rotation = Quaternion.Slerp(m_savedRotation, m_targetRotation, m_elapsedTime / m_timeToRotate);
		}

		private void CheckRotation()
		{
			if (CurrentState is Ledge or MainAttackStart) return;
			if(Direction ==  Vector3.zero) return;
			Vector3 inputRotation = Vector3.zero;
			inputRotation.y = Direction.x > 0f ? 0 : 179f;
			m_targetRotation = Quaternion.Euler(inputRotation);
		}
		
		private void HandleJump()
		{
			m_isClimbing = false;
			Vector3 verticalVelocity = m_tr.up * m_jumpPower;
			RemoveVerticalVelocity();
			m_savedVelocity += verticalVelocity;
			// Debug.Log(m_savedVelocity);
		}

		private void HandleLaunch()
		{
			RemoveVerticalVelocity();
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
			if (CurrentState is MainAttackStart) return;
			
			m_velocity = GetMovementVelocity();
			horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, m_velocity, 
				m_acceleration * Time.fixedDeltaTime);
		}

		private Vector3 GetMovementVelocity() => Direction * m_currentMoveSpeed;

		private void RemoveVerticalVelocity()
		{
			m_savedVelocity = Vector3Math.RemoveDotVector(m_savedVelocity, m_tr.up);
		}
		
		private void SetUpGraphics()
		{
			Instantiate(m_properties.characterModel, m_tr.position, Quaternion.identity, m_tr);
			var animator = GetComponent<Animator>();
			animator.avatar = m_properties.avatar;
			animator.runtimeAnimatorController = m_properties.animatorController;
		}

		private void SetUpAttackStrategies()
		{
			mainAttackStrategy = AttackStrategyFactory.CreateAttackStrategy()
				.WithScanner(m_properties.mainAttackStrategyData.ScanningStrategy, m_tr, m_targetLayers)
				.WithAbilityEffect(m_properties.mainAttackStrategyData.AbilityEffects);
			
			specialAttackStrategy = AttackStrategyFactory.CreateAttackStrategy()
				.WithScanner(m_properties.specialAttackStrategyData.ScanningStrategy, m_tr, m_targetLayers)
				.WithAbilityEffect(m_properties.specialAttackStrategyData.AbilityEffects);
			
		}

		private void SetUpTimers()
		{
			m_jumpBufferTimer = new CountDownTimer(m_jumpBufferTime);
			m_wallJumpBufferTimer = new CountDownTimer(m_jumpBufferTime);
		}

		private void SetUpVariables()
		{
			m_currentMoveSpeed = m_groundSpeed;
			m_gravity = m_airGravity;
			m_currentFallSpeed = m_maxFallSpeed;
			m_acceleration = m_groundAcceleration;
			m_numberOfJumps = m_maxNumberOfJumps;
		}

		private void SetUpComponents()
		{
			m_tr = transform;
			m_motor ??= GetComponent<PlayerMotor>();
			m_ledgeDetector ??= GetComponent<LedgeDetector>();
			m_wallDetector ??= GetComponent<WallDetector>();
			m_ceilingDetector ??= GetComponent<CeilingDetector>();
			m_graphicsController ??= GetComponent<PlayerGraphicsController>();
			SetTargetLayers();
		}

		private void SetUpStateMachine()
		{
			m_stateMachine = new StateMachine();

			m_groundedState = new GroundedSubStateMachine(this, m_graphicsController);
			m_airborneState = new AirborneSubStateMachine(this, m_graphicsController);
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
		
		public void SetTargetLayers()
		{
			int selfLayer = gameObject.layer;
			int layerMask = Physics.AllLayers;
		    
			for (int i = 0; i < 32; i++)
			{
				if (Physics.GetIgnoreLayerCollision(selfLayer, i))
					layerMask &= ~(1 << i);
			}
		    
			int ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
			layerMask &= ~(1 << ignoreLayer);

			m_targetLayers = layerMask;
		}

		private void AddTransition(IState from, IState to, IPredicate condition) =>
			m_stateMachine.AddTransition(from, to, condition);
		
		private void AddAnyTransition(IState to, IPredicate condition) => m_stateMachine.AddAnyTransition(to, condition);
		
		#endregion Private Methods
	}	
}

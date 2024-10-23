using UnityEngine;
using Smash.Player.StructsAndEnums;

namespace Smash.Player
{
	[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
    public class PlayerMotor : MonoBehaviour
    {
	    private const float k_safetyDistance = 0.001f;

	    [Header("Collider Settings")]
	    [SerializeField] private float m_height = 1.8f;
	    [SerializeField] private float m_radius = 0.35f;
	    [SerializeField] private float m_stepHeight = 0.35f;
	    [SerializeField, Range(0, 90)] private float m_slopeLimit = 45f;

	    private Rigidbody m_rb;
	    private Transform m_tr;
	    private CapsuleCollider m_col;
	    private CapsuleCastSensor m_sensor;

	    private bool m_useExtendedSensor = false;
	    
	    private bool m_isGrounded;
	    private bool m_isSliding;
	    private Vector3 m_currentGroundAdjustmentVelocity;
	    private int m_currentLayer;
	    private float m_baseSensorRange;

	    #region Unity Methods

	    private void Awake()
	    {
		    SetUp();
		    RecalculateColliderDimensions();
	    }

	    private void OnValidate()
	    {
		    if (gameObject.activeInHierarchy)
		    {
			    RecalculateColliderDimensions();
		    }
	    }

	    #endregion Unity Methods

	    #region Public Methods

	    public void CheckForGround()
	    {
		    if (m_currentLayer != gameObject.layer)
		    {
			    RecalculateSensorLayerMask();
		    }

		    m_currentGroundAdjustmentVelocity = Vector3.zero;
		    
		    m_sensor.castDistance = m_useExtendedSensor
			    ? m_baseSensorRange + m_stepHeight + k_safetyDistance
			    : m_baseSensorRange;
		    
		    m_sensor.Cast();
		    
		    m_isGrounded = m_sensor.HasDetectedHit();
		    // Debug.Log(m_sensor.GetHitAngle());
		    
		    if (!m_isGrounded) return;
		    
		    /*float hitAngle = m_sensor.GetHitAngle();
		    
		    if (hitAngle <= m_slopeLimit || hitAngle >= 180 - m_slopeLimit) return;*/
		    
		    m_currentGroundAdjustmentVelocity = m_tr.up * (-m_sensor.GetHitDistance() / Time.fixedDeltaTime);
	    }

	    public bool IsGrounded() => m_isGrounded;

	    public bool IsGroundTooSteep() =>
		    m_sensor.GetHitAngle() >= m_slopeLimit && m_sensor.GetHitAngle() <= 180 - m_slopeLimit;
	    
	    public Vector3 GetGroundNormal() => m_sensor.GetHitNormal();

	    public void SetVelocity(Vector3 velocity) => m_rb.linearVelocity = velocity + m_currentGroundAdjustmentVelocity;

	    public void SetExtendedSensor(bool useExtendedSensor) => m_useExtendedSensor = useExtendedSensor;

	    #endregion Public Methods

	    #region Private Methods

	    private void SetUp()
	    {
		    m_tr = transform;
		    m_rb = GetComponent<Rigidbody>();
		    m_col = GetComponent<CapsuleCollider>();

		    m_rb.freezeRotation = true;
		    m_rb.useGravity = false;
	    }

	    private void RecalculateColliderDimensions()
	    {
		    if (m_col == null) SetUp();

		    m_rb.useGravity = false;
		    m_rb.freezeRotation = true;

		    m_col.height = m_height;
		    m_col.radius = m_radius;
		    m_col.center = m_height * 0.5f * Vector3.up;

		    if (m_col.height * 0.5f < m_col.radius || m_col.radius <= 0f)
		    {
			    m_radius = m_radius <= 0 ? 0.01f : m_height * 0.5f;
			    m_col.radius = m_col.radius <= 0 ? 0.01f : m_col.height * 0.5f;
		    }

		    RecalibrateSensor();
	    }
	    
	    private void RecalibrateSensor()
	    {
		    m_sensor ??= new CapsuleCastSensor(m_tr, CastDirection.Down, m_height, m_radius);
		    m_sensor.SetCastOrigin(m_col.bounds.center);
		    m_sensor.SetCastDirection(CastDirection.Down);
		    
		    RecalculateSensorLayerMask();

		    m_baseSensorRange = m_radius * (1f + k_safetyDistance);
		    m_sensor.castDistance = m_baseSensorRange;
	    }
	    
	    private void RecalculateSensorLayerMask()
	    {
		    int objectLayer = gameObject.layer;
		    int layerMask = Physics.AllLayers;
		    
		    for (int i = 0; i < 32; i++)
		    {
			    if (Physics.GetIgnoreLayerCollision(objectLayer, i))
				    layerMask &= ~(1 << i);
		    }
		    
		    int ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
		    layerMask &= ~(1 << ignoreLayer);
		    
		    m_sensor.layerMask = layerMask;
		    m_currentLayer = layerMask;
	    }

	    #endregion Private Methods
    }
}

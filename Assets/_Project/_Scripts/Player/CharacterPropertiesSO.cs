using UnityEngine;

namespace Smash.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "Scriptable Objects/PlayerPropertiesSO")]
    public class CharacterPropertiesSO : ScriptableObject
    {
        #region Fields
        [SerializeField] private float m_timeToRotate = 0.1f;
        [Header("Ground")]
        [SerializeField] private float m_groundSpeed = 10f;
	    [SerializeField] private float m_groundAcceleration = 10f;
        [Header("Air")]
        [SerializeField] private float m_airSpeed = 5f;
        [SerializeField] private float m_airAcceleration = 5f;
        [Header("Jump")]
        [SerializeField] private float m_jumpPower = 10f;
        [SerializeField] private int m_numberOfJumps = 2;
        [Header("Dash")]
        [SerializeField] private float m_dashSpeed = 30f;
        [SerializeField] private float m_dashDuration = 0.5f;
        [SerializeField] private int m_numberOfDashes = 1;
        [Header("Launch")]
        [SerializeField] private float m_launchPower = 100f;
        [SerializeField] private float m_crashSpeed = 100f;
        [SerializeField] private float m_floatFallSpeed = 10f;
        [SerializeField, Range(0, 2)] private float m_floatControlRatio = 0.5f;
        
        #endregion

        #region Properties

        public float GroundSpeed => m_groundSpeed;
        public float GroundAcceleration => m_groundAcceleration;
        public float AirSpeed => m_airSpeed;
        public float AirAcceleration => m_airAcceleration;
        public float JumpPower => m_jumpPower;
        public int NumberOfJumps => m_numberOfJumps;
        public float DashSpeed => m_dashSpeed;
        public float DashDuration => m_dashDuration;
        public int NumberOfDashes => m_numberOfDashes;
        public float LaunchPower => m_launchPower;
        public float CrashSpeed => m_crashSpeed;
        public float FloatFallSpeed => m_floatFallSpeed;
        public float FloatControlRatio => m_floatControlRatio;
        public float TimeToRotate => m_timeToRotate;

        #endregion
    }
}

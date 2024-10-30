using UnityEngine;
using UnityEngine.Serialization;

namespace Smash.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "Scriptable Objects/PlayerPropertiesSO")]
    public class PlayerPropertiesSO : ScriptableObject
    {
        #region Fields
        [Header("Ground")]
        [SerializeField] private float m_groundSpeed = 10f;
        [SerializeField] private float m_groundGravity = 30f;
	    [SerializeField] private float m_groundAcceleration = 10f;
        [Header("Air")]
        [SerializeField] private float m_airSpeed = 5f;
        [SerializeField] private float m_airGravity = 30f;
        [SerializeField] private float m_airAcceleration = 5f;
        [SerializeField] private float m_maxFallSpeed = 10f;
        [Header("Jump")]
        [SerializeField] private float m_jumpPower = 10f;
        [SerializeField] private int m_numberOfJumps = 2;
        [Header("Dash")]
        [SerializeField] private float m_dashSpeed = 30f;
        [SerializeField] private float m_dashDuration = 0.5f;
        [SerializeField] private int m_numberOfDashes = 1;
        
        #endregion

        #region Properties

        public float GroundSpeed => m_groundSpeed;
        public float GroundGravity => m_groundGravity;
        public float GroundAcceleration => m_groundAcceleration;
        public float AirSpeed => m_airSpeed;
        public float AirGravity => m_airGravity;
        public float AirAcceleration => m_airAcceleration;
        public float MaxFallSpeed => m_maxFallSpeed;
        public float JumpPower => m_jumpPower;
        public int NumberOfJumps => m_numberOfJumps;
        public float DashSpeed => m_dashSpeed;
        public float DashDuration => m_dashDuration;
        public int NumberOfDashes => m_numberOfDashes;

        #endregion
    }
}

using UnityEngine;
using UnityEngine.Serialization;

namespace Smash.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "Scriptable Objects/PlayerPropertiesSO")]
    public class PlayerPropertiesSO : ScriptableObject
    {
        #region Fields

        [SerializeField] private float m_groundSpeed = 10f;
        [SerializeField] private float m_airSpeed = 5f;
        [SerializeField] private float m_groundGravity = 30f;
        [SerializeField] private float m_airGravity = 30f;
        [SerializeField] private float m_maxFallSpeed = 10f;
	    [SerializeField] private float m_groundAcceleration = 10f;
        [SerializeField] private float m_airAcceleration = 5f;
        [SerializeField] private float m_jumpPower = 10f;
        // [SerializeField] private float m_mass = 10f;
        [SerializeField] private int m_numberOfJumps = 2;
        
        #endregion

        #region Properties

        public float GroundSpeed => m_groundSpeed;
        public float AirSpeed => m_airSpeed;
        public float AirGravity => m_airGravity;
        public float GroundGravity => m_groundGravity;
        public float MaxFallSpeed => m_maxFallSpeed;
        public float GroundAcceleration => m_groundAcceleration;
        public float AirAcceleration => m_airAcceleration;
        public int NumberOfJumps => m_numberOfJumps;
        public float JumpPower => m_jumpPower;

        #endregion
    }
}

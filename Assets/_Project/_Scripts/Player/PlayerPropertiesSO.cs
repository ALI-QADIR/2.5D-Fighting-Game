using UnityEngine;
using UnityEngine.Serialization;

namespace Smash
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "Scriptable Objects/PlayerPropertiesSO")]
    public class PlayerPropertiesSO : ScriptableObject
    {
        #region Fields

        [SerializeField] private float m_movementSpeed = 10f;
        [SerializeField] private float m_airControlRate = 5f;
        [SerializeField] private float m_gravity = 30f;
        [SerializeField] private float m_maxFallSpeed = 10f;
        
        #endregion

        #region Properties

        public float MovementSpeed => m_movementSpeed;
        public float AirControlRate => m_airControlRate;
        public float Gravity => m_gravity;
        public float MaxFallSpeed => m_maxFallSpeed;

        #endregion
    }
}

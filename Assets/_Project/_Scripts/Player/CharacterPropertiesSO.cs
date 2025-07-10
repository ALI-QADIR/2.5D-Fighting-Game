using Smash.Player.AttackStrategies;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Smash.Player
{
    [CreateAssetMenu(fileName = "CharacterProperties", menuName = "Scriptable Objects/Character Properties")]
    public class CharacterPropertiesSO : ScriptableObject
    {
        public string characterName = "X Bot";
        public GameObject characterModel;
        public AnimatorController animatorController;
        public Avatar avatar;
        public LayerMask targetLayers;

        [Header("Attack Strategies")] 
        public AttackStrategyData mainAttackStrategyData;
        public AttackStrategyData sideMainAttackStrategyData;
        public AttackStrategyData specialAttackStrategyData;
    }
}

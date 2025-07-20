using Smash.Player.AbilityStrategies;
using UnityEditor.Animations;
using UnityEngine;

namespace Smash.Player
{
    [CreateAssetMenu(fileName = "CharacterProperties", menuName = "Scriptable Objects/Character Properties")]
    public class CharacterPropertiesSO : ScriptableObject
    {
        public string characterName = "X Bot";
        public GameObject characterModel;
        public AnimatorController animatorController;
        public Avatar avatar;

        [Header("Attack Strategies")] 
        public AbilityStrategyData mainAbilityStrategyData;
        public AbilityStrategyData sideMainAbilityStrategyData;
        public AbilityStrategyData upMainAbilityStrategyData;
        public AbilityStrategyData specialAbilityStrategyData;
        public AbilityStrategyData upSpecialAbilityStrategyData;
    }
}

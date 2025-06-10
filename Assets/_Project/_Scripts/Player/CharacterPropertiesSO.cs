using Smash.Player.AttackStrategies;
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
        public LayerMask targetLayers;

        [Header("Attack Strategies")] 
        public AttackStrategy mainAttackStrategy;

        public void SetTargetLayers(int selfLayer)
        {
            int layerMask = Physics.AllLayers;
		    
            for (int i = 0; i < 32; i++)
            {
                if (Physics.GetIgnoreLayerCollision(selfLayer, i))
                    layerMask &= ~(1 << i);
            }
		    
            int ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
            layerMask &= ~(1 << ignoreLayer);

            targetLayers = layerMask;
        }
    }
}

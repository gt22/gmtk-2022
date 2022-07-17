using Dice;
using Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameSituations.CustomSituations
{
    public class ForestIncreasedProductionSituation : MonoBehaviour, ICustomGameSituation
    {

        [ShowInInspector, SerializeField, FoldoutGroup("References")]
        private AspectEffectManager effects;
        
        public void SituationEffects(IDice dice)
        {
            Debug.Log($"Forest: {dice}");
            effects.AddEffect(new SelfRemovingEffect(IEffect.Additive, s =>
            {
                int r = dice.Roll();
                Debug.Log($"Supplies: {r}");
                s.Supplies += r;
            }));
        }
    }
}
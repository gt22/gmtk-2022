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

        public void SituationEffects(IDice dice, GameObject from)
        {
            if (effects == null) effects = from.GetComponent<AspectEffectManager>();

            Debug.Log($"Forest: {dice}", this);
            effects.AddEffect(new SelfRemovingEffect(IEffect.Additive, s =>
            {
                int r = dice.Roll();
                Debug.Log($"Supplies: {r}");
                s.Supplies += r;
            }));
        }
    }
}
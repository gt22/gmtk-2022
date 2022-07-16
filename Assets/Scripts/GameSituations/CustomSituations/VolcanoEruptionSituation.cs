using UnityEngine;

namespace GameSituations.CustomSituations
{
    public class VolcanoEruptionSituation : MonoBehaviour, ICustomGameSituation
    {
        public void SituationEffects()
        {
            Debug.Log("Volcano Eruption");
        }
    }
}
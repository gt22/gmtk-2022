using UnityEngine;

namespace GameSituations.CustomSituations
{
    public class OceanInvasionSituation : MonoBehaviour, ICustomGameSituation
    {
        public void SituationEffects()
        {
            Debug.Log("Ocean Invasion");
        }
    }
}
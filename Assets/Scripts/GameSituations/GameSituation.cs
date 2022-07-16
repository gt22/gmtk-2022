using Global;
using UnityEngine;

namespace GameSituations
{
    public class GameSituation : MonoBehaviour
    {
        public int turnsUntilTrigger;
        private int _turnsToTrigger;

        private void OnEnable()
        {
            _turnsToTrigger = turnsUntilTrigger;
        }

        public bool IsReady()
        {
            return _turnsToTrigger == 0;
        }

        public void TurnEnd()
        {
            _turnsToTrigger--;
        }

        public void SituationEffect()
        {
            GetComponent<ICustomGameSituation>().SituationEffects();
        }
    }
}
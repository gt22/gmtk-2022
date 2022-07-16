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
            TurnHandler.OnAfterTurnEnd += ReduceTurns;
        }

        private void OnDisable()
        {
            TurnHandler.OnAfterTurnEnd -= ReduceTurns;
        }

        private void ReduceTurns()
        {
            _turnsToTrigger--;
            if (_turnsToTrigger == 0)
                SituationEffect();
        }

        private void SituationEffect()
        {
            GetComponent<ICustomGameSituation>().SituationEffects();
        }
    }
}
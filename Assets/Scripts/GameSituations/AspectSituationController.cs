using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameSituations
{
    public class AspectSituationController : MonoBehaviour
    {
        public List<GameSituation> possibleSituation;

        [ShowInInspector, FoldoutGroup("Debug Info"), ReadOnly]
        private List<GameSituation> _forcedSituations = new();

        public GameSituation NextSituation { get; private set; }

        private void SelectNextSituation()
        {
            NextSituation = _forcedSituations.FirstOrDefault(x => x.IsReady()) ??
                            possibleSituation[Random.Range(0, possibleSituation.Count)];
            Debug.Log(NextSituation);
        }

        public void TurnUpdate()
        {
            foreach (var s in _forcedSituations)
            {
                s.TurnEnd();
            }

            SelectNextSituation();
        }
    }
}
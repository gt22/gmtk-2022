using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameSituations
{
    public class AspectSituationController : MonoBehaviour
    {
        public List<GameSituation> possibleSituation = new List<GameSituation>();

        [ShowInInspector, FoldoutGroup("Debug Info"), ReadOnly]
        private List<GameSituation> _forcedSituations;
        
        
    }
}
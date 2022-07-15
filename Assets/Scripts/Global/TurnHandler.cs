using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global
{
    public class TurnHandler : MonoBehaviour
    {
        public static event Action OnTurnEnd;

        [Button("Test Ending Turn"), BoxGroup("Testing")]
        public void EndTurn()
        {
            OnTurnEnd?.Invoke();
        }
    }
}
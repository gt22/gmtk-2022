using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global
{
    public class TurnHandler : MonoBehaviour
    {
        public bool callTurnBeginOnAwake = true;

        public static event Action OnBeforeTurnBegin;
        public static event Action OnTurnBegin;
        public static event Action OnTurnEnd;
        public static event Action OnAfterTurnEnd;

        private bool _turnInProgress;

        private void Awake()
        {
            BeginTurn();
        }

        [Button("Test Begin Turn"), BoxGroup("Testing")]
        public void BeginTurn()
        {
            if (_turnInProgress)
            {
                Debug.LogError("Turn is already in progress!", this);
                return;
            }

            OnBeforeTurnBegin?.Invoke();
            OnTurnBegin?.Invoke();
            _turnInProgress = true;
        }

        [Button("Test Ending Turn"), BoxGroup("Testing")]
        public void EndTurn()
        {
            if (!_turnInProgress)
            {
                Debug.LogError("Turn has not started yet!", this);
                return;
            }

            OnTurnEnd?.Invoke();
            OnAfterTurnEnd?.Invoke();
        }
    }
}
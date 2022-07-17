using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global
{
    public class TurnHandler : MonoBehaviour
    {
        public bool callTurnBeginOnStart = true;
        public bool callTurnBeginAfterEnd = true;
        public bool logTurns = true;

        public static event Action OnBeforeTurnBegin;
        public static event Action OnTurnBegin;
        public static event Action OnTurnEnd;
        public static event Action OnAfterTurnEnd;

        private bool _turnInProgress;

        private void Start()
        {
            if (callTurnBeginOnStart)
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

            if (logTurns) Debug.LogWarning("BeginTurn!");

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

            if (logTurns) Debug.LogWarning("EndTurn!");

            OnTurnEnd?.Invoke();
            OnAfterTurnEnd?.Invoke();
            _turnInProgress = false;

            if (callTurnBeginAfterEnd)
                BeginTurn();
        }
    }
}
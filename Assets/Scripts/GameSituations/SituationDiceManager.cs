using System.Collections.Generic;
using System.Linq;
using Dice;
using Global;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameSituations
{
    public class SituationDiceManager : MonoBehaviour
    {

        private readonly List<IDice> _baseDice = new()
        {
            new SimpleDie(4),
            new SimpleDie(6),
            new SimpleDie(8),
            new SimpleDie(10),
            new SimpleDie(12),
            new SimpleDie(20)
        };

        private Queue<IDice> _freeDice;

        private void OnEnable()
        {
            TurnHandler.OnBeforeTurnBegin += ResetDice;
        }

        private void OnDisable()
        {
            TurnHandler.OnBeforeTurnBegin -= ResetDice;
        }

        private void ResetDice()
        {
            _freeDice = new Queue<IDice>(_baseDice.OrderBy(x => Random.value));
        }

        public IDice RequestSituationDice()
        {
            return _freeDice.Dequeue();
        }
    }
}
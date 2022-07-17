using Dice;
using UnityEngine;

namespace GameSituations
{
    public interface ICustomGameSituation
    {
        public abstract void SituationEffects(IDice dice, GameObject from);
    }
}
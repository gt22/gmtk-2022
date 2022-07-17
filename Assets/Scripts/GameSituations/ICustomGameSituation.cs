using Dice;

namespace GameSituations
{
    public interface ICustomGameSituation
    {
        public abstract void SituationEffects(IDice dice);
    }
}
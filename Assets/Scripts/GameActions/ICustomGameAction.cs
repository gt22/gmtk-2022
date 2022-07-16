using Dice;
using Resources;

namespace GameActions
{
    public interface ICustomGameAction
    {
        public void ActionEffects(IDice die);

        public bool CheckRequirements();
        
        public string Label { get; }
        public string ReqDescription { get; }
        public string EffectDescription { get; }
    }
}
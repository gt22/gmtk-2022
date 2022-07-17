using Resources;

namespace Effects
{
    public interface IEffect
    {
        public const int Multiplicative = 1;
        public const int Additive = 2;
        
        public int Priority { get; }
        public void Affect(ResourceStack income);

        public void TurnBegin();
        public void AddedTo(AspectEffectManager manager);
    }
}
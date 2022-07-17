using System;
using Resources;

namespace Effects
{
    public class SimpleEffect : IEffect
    {
        public int Priority { get; }
        private readonly Action<ResourceStack> _act;
        public SimpleEffect(int priority, Action<ResourceStack> act)
        {
            Priority = priority;
            _act = act;
        }

        public void Affect(ResourceStack income)
        {
            _act(income);
        }

        public virtual void TurnBegin() { }
        public virtual void AddedTo(AspectEffectManager manager) { }
    }
}
using System;
using Global;
using Resources;

namespace Effects
{
    public class SelfRemovingEffect : SimpleEffect
    {
        private int _turnsRemaining;
        private AspectEffectManager _manager;
        
        public SelfRemovingEffect(int priority, Action<ResourceStack> act, int turnsRemaining) : base(priority, act)
        {
            _turnsRemaining = turnsRemaining;
        }

        public SelfRemovingEffect(int priority, Action<ResourceStack> act) : this(priority, act, 1)
        {
            
        }

        public override void TurnBegin()
        {
            // _turnsRemaining--;
            // if (_turnsRemaining == 0 && _manager != null)
            // {
            //     _manager.RemoveEffect(this);
            // }
        }

        public override void AddedTo(AspectEffectManager manager)
        {
            _manager = manager;
        }
    }
}
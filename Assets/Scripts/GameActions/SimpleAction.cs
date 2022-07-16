using Dice;
using Resources;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameActions
{
    //TODO: Это валидно?
    public abstract class SimpleAction : MonoBehaviour, ICustomGameAction
    {
        [FoldoutGroup("References"), SerializeField, ShowInInspector]
        private ResourceManager manager;
        
        protected abstract ResourceStack Cost { get; }
        protected abstract ResourceStack Effect { get; }
        
        public void ActionEffects(IDice die)
        {
            manager.ModifyResources(Effect);
        }

        public bool CheckRequirements()
        {
            return manager.PlayerResources.Contains(Cost);
        }

        public abstract string Label { get; }
        public abstract string ReqDescription { get; }
        public abstract string EffectDescription { get; }
    }
}
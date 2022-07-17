using System.Collections.Generic;
using Resources;
using UnityEngine;

namespace Effects
{
    public class AspectEffectManager : MonoBehaviour
    {
        private List<IEffect> _effects = new List<IEffect>();

        public void AddEffect(IEffect effect)
        {
            _effects.Add(effect);
            effect.AddedTo(this);
            _effects.Sort((e1, e2) => e1.Priority.CompareTo(e2.Priority));
            Debug.Log($"Effect added: {GetHashCode()}, {_effects.Count}");
        }

        public void RemoveEffect(IEffect effect)
        {
            _effects.Remove(effect);
            Debug.Log($"Effect removes: {GetHashCode()}, {_effects.Count}");
        }

        public void Apply(ResourceStack income)
        {
            Debug.Log($"Effects: {GetHashCode()}, {_effects.Count}");
            foreach (var e in _effects)
            {
                Debug.Log($"Effect: {e}");
                e.Affect(income);
            }
        }
    }
}
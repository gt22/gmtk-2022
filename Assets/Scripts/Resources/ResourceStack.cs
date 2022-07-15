using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace Resources
{
    public class ResourceStack
    {
        private readonly Dictionary<ResourceType, int> _resources = new();

        public int Materials
        {
            get => _resources[ResourceType.MATERIALS];
            set => _resources[ResourceType.MATERIALS] = value;
        }
        
        public int Manpower
        {
            get => _resources[ResourceType.MANPOWER];
            set => _resources[ResourceType.MANPOWER] = value;
        }
        
        public int Supplies
        {
            get => _resources[ResourceType.SUPPLIES];
            set => _resources[ResourceType.SUPPLIES] = value;
        }

        public ResourceStack()
        {
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                _resources[type] = 0;
            }
        }

        public bool Contains(ResourceStack other)
        {
            return other._resources.All(pair => pair.Value < _resources[pair.Key]);
        }

        public void PourFrom(ResourceStack other)
        {
            foreach (var (key, value) in other._resources)
            {
                _resources[key] += value;
            }
        }

        public void PourTo(ResourceStack other)
        {
            other.PourFrom(this);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resources
{
    public class ResourceStack
    {
        private readonly Dictionary<ResourceType, int> _resources = new();

        public int Materials
        {
            get => this[ResourceType.MATERIALS];
            set => this[ResourceType.MATERIALS] = value;
        }
        
        public int Manpower
        {
            get => this[ResourceType.MANPOWER];
            set => this[ResourceType.MANPOWER] = value;
        }
        
        public int Supplies
        {
            get => this[ResourceType.SUPPLIES];
            set => this[ResourceType.SUPPLIES] = value;
        }

        public ResourceStack()
        {
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                _resources[type] = 0;
            }
        }
        public int this[ResourceType type]
        {
            get => _resources[type];
            set => _resources[type] = value;
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
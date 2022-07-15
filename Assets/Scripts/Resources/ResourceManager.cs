using System.Collections.Generic;

namespace Resources
{
    public class ResourceManager //TODO: MonoBehaviour?, integrate with unity
    {

        public readonly ResourceStack PlayerResources = new();

        private readonly List<ResourceStack> _incomeQueue = new();

        public void QueueIncome(ResourceStack income)
        {
            _incomeQueue.Add(income);
        }

        public void ModifyResources(ResourceStack res)
        {
            res.PourTo(PlayerResources);
            DisplayChange(res);
        }

        public void EndTurn()
        {
            var totalIncome = new ResourceStack();
            foreach (var income in _incomeQueue)
            {
                totalIncome.PourFrom(income);
            }
            totalIncome.PourFrom(PlayerResources);
            DisplayChange(totalIncome);
        }

        public void DisplayChange(ResourceStack s)
        {
            //TODO: Display
        }

    }
}
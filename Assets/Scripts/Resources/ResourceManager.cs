using System;
using System.Collections.Generic;
using Global;
using UnityEngine;

namespace Resources
{
    public class ResourceManager : MonoBehaviour
    {
        public readonly ResourceStack PlayerResources = new();

        private readonly List<ResourceStack> _incomeQueue = new();

        public static event Action<ResourceStack> OnUpdateResources;

        private void OnEnable()
        {
            TurnHandler.OnAfterTurnEnd += EndTurn;
        }

        private void OnDisable()
        {
            TurnHandler.OnAfterTurnEnd -= EndTurn;
        }

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

            ModifyResources(totalIncome);
        }

        public void DisplayChange(ResourceStack s)
        {
            OnUpdateResources?.Invoke(s);
        }
    }
}
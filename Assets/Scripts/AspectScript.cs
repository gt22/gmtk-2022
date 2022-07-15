using Global;
using Resources;
using Sirenix.OdinInspector;
using UnityEngine;

public class AspectScript : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField, ShowInInspector]
    private ResourceManager manager;

    [FoldoutGroup("Resources")] public ResourceType resourceGenerationType;

    [FoldoutGroup("Resources")] public int resourceGenerateAmount = 1;

    [FoldoutGroup("Resources"), ReadOnly, ShowInInspector]
    private bool _canGenerateResources = true;

    private void OnEnable()
    {
        TurnHandler.OnTurnEnd += AspectTurnEndEffects;
    }

    private void OnDisable()
    {
        TurnHandler.OnTurnEnd -= AspectTurnEndEffects;
    }

    private void AddResources()
    {
        // TODO
        //manager.QueueIncome();
        // new ResourceStack(resourceGenerationType, resourceGenerateAmount);
    }

    private void AspectTurnEndEffects()
    {
        Debug.Log($"{gameObject.name} received TurnEnd call!", this);

        AddResources();
    }
}
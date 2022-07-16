using GameSituations;
using Global;
using Resources;
using Sirenix.OdinInspector;
using UnityEngine;

public class AspectScript : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField, ShowInInspector]
    private ResourceManager manager;

    private AspectSituationController situationController;

    [FoldoutGroup("Resources")] public ResourceType resourceGenerationType;

    [FoldoutGroup("Resources")] public int resourceGenerateAmount = 1;

    [FoldoutGroup("Resources"), ReadOnly, ShowInInspector]
    private bool _canGenerateResources = true;

    private void OnEnable()
    {
        TurnHandler.OnTurnBegin += AspectTurnBeginEffect;
        TurnHandler.OnTurnEnd += AspectTurnEndEffects;
        situationController = GetComponent<AspectSituationController>();
    }

    private void OnDisable()
    {
        TurnHandler.OnTurnEnd -= AspectTurnEndEffects;
        TurnHandler.OnTurnBegin -= AspectTurnBeginEffect;
    }

    private void AddResources()
    {
        if (_canGenerateResources)
            manager.QueueIncome(resourceGenerationType.Stack(resourceGenerateAmount));
    }

    private void AspectTurnBeginEffect()
    {
        situationController.TurnUpdate();
    }

    private void AspectTurnEndEffects()
    {
        situationController.NextSituation.SituationEffect();
        AddResources();
    }
}
using Dice;
using Effects;
using GameSituations;
using Global;
using Resources;
using Sirenix.OdinInspector;
using UnityEngine;

public class AspectScript : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField, ShowInInspector]
    private ResourceManager manager;

    [FoldoutGroup("References"), SerializeField, ShowInInspector]
    private SituationDiceManager situationDiceManager;

    private AspectSituationController _situationController;
    private AspectEffectManager _effectManager;
    private IDice _situationDice;

    [FoldoutGroup("Resources")] public ResourceType resourceGenerationType;

    [FoldoutGroup("Resources")] public int resourceGenerateAmount = 1;

    [FoldoutGroup("Resources"), ReadOnly, ShowInInspector]
    private bool _canGenerateResources = true;

    private void OnEnable()
    {
        //Debug.Log($"{gameObject.name}: OnEnable", this);
        TurnHandler.OnTurnBegin += AspectTurnBeginEffect;
        TurnHandler.OnTurnEnd += AspectTurnEndEffects;
        _situationController = GetComponent<AspectSituationController>();
        _effectManager = GetComponent<AspectEffectManager>();
    }

    private void OnDisable()
    {
        //Debug.Log($"{gameObject.name}: OnDisable", this);

        TurnHandler.OnTurnEnd -= AspectTurnEndEffects;
        TurnHandler.OnTurnBegin -= AspectTurnBeginEffect;
    }

    private void AddResources()
    {
        if (!_canGenerateResources) return;
        var income = resourceGenerationType.Stack(resourceGenerateAmount);
        Debug.Log($"Income: {income}", this);
        _effectManager.Apply(income);
        Debug.Log($"After effects: {income}", this);
        manager.QueueIncome(income);
    }

    private void AspectTurnBeginEffect()
    {
        //Debug.Log($"{gameObject.name}: TurnBeginEffect", this);
        _situationDice = situationDiceManager.RequestSituationDice();
        _situationController.TurnUpdate();
    }

    private void AspectTurnEndEffects()
    {
        _situationController.NextSituation.SituationEffect(_situationDice, gameObject);
        AddResources();
    }
}
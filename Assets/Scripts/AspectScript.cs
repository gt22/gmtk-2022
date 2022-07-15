using Global;
using UnityEngine;

public class AspectScript : MonoBehaviour
{
    private void OnEnable()
    {
        TurnHandler.OnTurnEnd += AspectTurnEndEffects;
    }

    private void OnDisable()
    {
        TurnHandler.OnTurnEnd -= AspectTurnEndEffects;
    }

    private void AspectTurnEndEffects()
    {
        Debug.Log($"{gameObject.name} received TurnEnd call!", this);
    }
}
using UnityEngine;

public static class GlobalSelectablesController
{
    private static GameObject _selectedUnit = null;

    // Returns PREVIOUSLY SELECTED OBJECT
    public static GameObject Select(GameObject o)
    {
        var prev = _selectedUnit;
        _selectedUnit = o;
        return prev;
    }

    public static GameObject GetSelected() => _selectedUnit;
    public static void ResetSelected() => _selectedUnit = null;
}
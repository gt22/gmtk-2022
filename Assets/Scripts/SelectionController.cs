using Sirenix.OdinInspector;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    private SpriteRenderer _renderer;

    [HideInPlayMode, FoldoutGroup("Unlit Outline")]
    public bool useAdditionalRendererOutline = false;

    [ShowIf("useAdditionalRendererOutline"), FoldoutGroup("Unlit Outline")]
    public GameObject secondRendererObject;

    [ShowIf("useAdditionalRendererOutline"), FoldoutGroup("Unlit Outline")]
    public Material outlineMaterial;

    private SpriteRenderer _outlineRenderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer != null) _renderer.material = Instantiate(_renderer.material);

        // If we render outline via second sprite renderer, get it
        InitSecondRenderer();
    }

    private void InitSecondRenderer()
    {
        if (!useAdditionalRendererOutline) return;

        //_outlineRenderer = secondRendererObject.AddComponent(gameObject.GetComponent<SpriteRenderer>());
        _outlineRenderer = secondRendererObject.GetComponent<SpriteRenderer>();
        _outlineRenderer.enabled = false;
        _outlineRenderer.material = Instantiate(outlineMaterial);
    }

    // ======================== [SELECTION] ========================

    public bool mouseHoverSelectable = false;
    private static readonly int OutlineOpacity = Shader.PropertyToID("_OutlineOpacity");


    private enum HoverState
    {
        None,
        Highlighted,
        Selected
    }

    private HoverState _hoverState = HoverState.None;

    [FoldoutGroup("Unit Selection"), ColorUsage(true, true)]
    public Color mouseHoverSelectionColor;

    [FoldoutGroup("Unit Selection"), ColorUsage(true, true)]
    public Color selectedColor;

    public void DeselectUnit(bool reset = false)
    {
        //if (reset) GlobalUnitController.ResetSelected();

        DisableSelectedOutline();
        //_cameraScript.FocusOnDefaultCameraPoint();
        _hoverState = HoverState.None;
        DisableSelectedOutline();

        /*switch (_type)
        {
            case GlobalUnitController.UnitType.PlayerUnit:
                DeselectUnitEffects();
                break;

            case GlobalUnitController.UnitType.StructureSite:
                DeselectStructureSiteEffects();
                break;

            case GlobalUnitController.UnitType.StructureUnit:
                DeselectStructureEffects();
                break;

            case GlobalUnitController.UnitType.EnemyUnit:
                break;
        }*/
    }

    /*private void DeselectUnitEffects()
    {
        if (!_useDividedScripts)
        {
            if (!_mergedUnitController) return;
            _mergedUnitController.SetPlayerControls(false);
            _mergedUnitController.InitAIState();
        }
        else
        {
            if (!_unitController || !_AIController) return;
            //_unitController.SetPlayerControls(false);
            _unitController.playerControllable = false;
            _unitController.currentlyPlayerControllable = false;
            _AIController.InitAIState();
        }
    }*/

    /*private void SelectStructureSiteEffects()
    {
        //GetComponent<UIOptionsScript>()?.SpawnButtons();
        GetComponent<StructureSiteScript>().ExpandSelection();
        Debug.LogWarning("Stub implementation!");
    }

    private void DeselectStructureSiteEffects()
    {
        GetComponent<UIOptionsScript>()?.CollapseButtons();
        Debug.LogWarning("Stub implementation!");
    }

    private void SelectStructureEffects()
    {
        GetComponent<UIOptionsScript>().SpawnButtons();
        Debug.LogWarning("Stub implementation!");
    }

    private void DeselectStructureEffects()
    {
        GetComponent<UIOptionsScript>()?.CollapseButtons();
        Debug.LogWarning("Stub implementation!");
    }*/

    private void HandlePrev(GameObject prev)
    {
        EnableSelectedOutline();
        if (prev == null) return;
        prev.GetComponent<SelectionController>()?.DisableSelectedOutline();
        prev.GetComponent<SelectionController>()?.DeselectUnit();
    }

    [ShowInInspector, ReadOnly, FoldoutGroup("Unit Selection")]
    private bool Selected => GlobalSelectablesController.GetSelected() == gameObject;

    [Button]
    private void Select()
    {
        DisableHoverOutline();

        var previous = GlobalSelectablesController.Select(gameObject);
        HandlePrev(previous);

        //_cameraScript.FocusOnCameraPoint(cameraPoint, _cameraScript.pointTransitionDurationPublic);

        /*switch (_type)
        {
            case GlobalUnitController.UnitType.PlayerUnit:
                SelectUnitEffects();
                break;

            case GlobalUnitController.UnitType.StructureSite:
                SelectStructureSiteEffects();
                break;

            case GlobalUnitController.UnitType.StructureUnit:
                SelectStructureEffects();
                break;

            case GlobalUnitController.UnitType.EnemyUnit:
                break;
        }*/
    }

    public void _OnMouseClick()
    {
        if (_hoverState == HoverState.Highlighted)
            Select();
    }

    /*private void SelectUnitEffects()
    {
        if (!_useDividedScripts)
        {
            _mergedUnitController.ExitState();
            _mergedUnitController.SetPlayerControls(true);
            _mergedUnitController.ResetLocalScaleX();
        }
        else
        {
            _AIController.ExitState();
            _unitController.SetPlayerControls(true);
            _unitController.ResetLocalScaleX();
        }
    }*/

    public void _OnMouseOver()
    {
    }

    public void _OnMouseExit()
    {
        DisableHoverOutline();
    }

    public void _OnMouseEnter()
    {
        EnableHoverOutline();
    }

    public void DisableSelectedOutline()
    {
        if (!useAdditionalRendererOutline)
        {
            _renderer.material.SetFloat(OutlineOpacity, 0f);
            //_hoverState = HoverState.None;
        }
        else
        {
            _outlineRenderer.enabled = false;
        }

        _hoverState = HoverState.None;
    }

    public void EnableSelectedOutline()
    {
        if (!useAdditionalRendererOutline)
        {
            _renderer.material.SetColor(OutlineColor, selectedColor);
            _renderer.material.SetFloat(OutlineOpacity, 1f);
            //_hoverState = HoverState.Selected;
        }
        else
        {
            _outlineRenderer.material.SetColor(OutlineColor, selectedColor);
            _outlineRenderer.enabled = true;
        }

        _hoverState = HoverState.Selected;
    }

    public void EnableHoverOutline()
    {
        if (_hoverState == HoverState.None && mouseHoverSelectable)
        {
            if (!useAdditionalRendererOutline)
            {
                _renderer.material.SetColor(OutlineColor, mouseHoverSelectionColor);
                _renderer.material.SetFloat(OutlineOpacity, 1f);
            }
            else
            {
                _outlineRenderer.enabled = true;
                _outlineRenderer.material.SetColor(OutlineColor, mouseHoverSelectionColor);
            }


            _hoverState = HoverState.Highlighted;
        }
    }

    public void DisableHoverOutline()
    {
        if (_hoverState == HoverState.Highlighted && mouseHoverSelectable)
        {
            if (!useAdditionalRendererOutline)
            {
                _renderer.material.SetFloat(OutlineOpacity, 0f);
            }
            else
            {
                _outlineRenderer.enabled = false;
            }

            _hoverState = HoverState.None;
        }
    }

    private static readonly int OutlineColor = Shader.PropertyToID("Color_74F00D25");
    //private static readonly int OutlineAltColor = Shader.PropertyToID("_OutlineAltColor");

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _OnMouseClick();

        if (Input.GetKeyDown(KeyCode.Escape) && Selected)
            DeselectUnit(true);
    }

    private Color _prevColor;
}
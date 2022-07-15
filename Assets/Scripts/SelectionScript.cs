using Sirenix.OdinInspector;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    private enum HoverState
    {
        None,
        Highlighted,
        Selected
    }

    public Sprite defaultSprite;
    public Sprite selectedSprite;
    public Sprite hoverSprite;

    [ShowInInspector, ReadOnly] private HoverState _hoverState = HoverState.None;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void EnableSelectedOutline()
    {
        _renderer.sprite = selectedSprite;
    }

    private void DisableSelectedOutline()
    {
        _renderer.sprite = defaultSprite;
    }

    private void DisableHoverOutline()
    {
        _renderer.sprite = defaultSprite;
    }

    private void EnableHoverOutline()
    {
        _renderer.sprite = hoverSprite;
    }

    // These are USED, just called by RaycastController
    public void _OnMouseOver()
    {
    }

    public void _OnMouseExit()
    {
        if (_hoverState == HoverState.Highlighted)
        {
            DisableHoverOutline();
            _hoverState = HoverState.None;
        }
    }

    public void _OnMouseEnter()
    {
        if (_hoverState == HoverState.None)
        {
            EnableHoverOutline();
            _hoverState = HoverState.Highlighted;
        }
    }

    public void DeselectUnit(bool reset = false)
    {
        DisableSelectedOutline();
        _hoverState = HoverState.None;
    }

    private void HandlePrev(GameObject prev)
    {
        EnableSelectedOutline();
        _hoverState = HoverState.Selected;

        if (prev == null) return;
        prev.GetComponent<SelectionScript>()?.DisableSelectedOutline();
        prev.GetComponent<SelectionScript>()?.DeselectUnit();
    }

    private void Select()
    {
        DisableHoverOutline();

        var previous = GlobalSelectablesController.Select(gameObject);
        HandlePrev(previous);
    }

    public void _OnMouseClick()
    {
        switch (_hoverState)
        {
            case HoverState.Highlighted:
                Select();
                break;
            case HoverState.Selected:
                DeselectUnit();
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _OnMouseClick();

        if (Input.GetKeyDown(KeyCode.Escape) && Selected)
            DeselectUnit(true);
    }

    private bool Selected => GlobalSelectablesController.GetSelected() == gameObject;
}
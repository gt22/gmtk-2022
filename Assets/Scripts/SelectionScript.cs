using Sirenix.OdinInspector;
using UI;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    private enum HoverState
    {
        None,
        Highlighted,
        Selected
    }

    public bool showActionCard = true;

    public Sprite defaultSprite;
    public Sprite selectedSprite;
    public Sprite hoverSprite;

    [ShowInInspector, ReadOnly, FoldoutGroup("Debug Info")]
    private HoverState _hoverState = HoverState.None;

    [ShowInInspector, ReadOnly, FoldoutGroup("Debug Info")]
    private bool _hasActionCard;

    [ShowInInspector, ReadOnly, FoldoutGroup("Debug Info")]
    private bool _isMouseOver;

    private SpriteRenderer _renderer;
    private ActionCardHandler _cardHandler;

    private void Start()
    {
        _cardHandler = GetComponent<ActionCardHandler>();
        _hasActionCard = _cardHandler;

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
        _isMouseOver = false;
        if (_hoverState == HoverState.Highlighted)
        {
            DisableHoverOutline();
            _hoverState = HoverState.None;
        }
    }

    public void _OnMouseEnter()
    {
        _isMouseOver = true;
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

        if (showActionCard && _hasActionCard) _cardHandler.HideCard();
    }

    private void HandlePrev(GameObject prev)
    {
        EnableSelectedOutline();
        _hoverState = HoverState.Selected;

        if (prev == null || prev == gameObject) return;
        prev.GetComponent<SelectionScript>()?.DisableSelectedOutline();
        prev.GetComponent<SelectionScript>()?.DeselectUnit();
    }

    private void Select()
    {
        DisableHoverOutline();

        var previous = GlobalSelectablesController.Select(gameObject);
        HandlePrev(previous);

        if (showActionCard && _hasActionCard) _cardHandler.ShowCard();
    }

    public void _OnMouseClick()
    {
        //Debug.Log($"{gameObject.name}: {_hoverState}", this);
        switch (_hoverState)
        {
            case HoverState.Highlighted:
                Select();
                break;
            case HoverState.Selected:
                if (_isMouseOver) DeselectUnit();
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
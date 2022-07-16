using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DraggableElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,
        IPointerClickHandler
    {
        private CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;
        private bool _isDragged;
        //protected UIInventory Inventory;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>();
            //Inventory = GetComponentInParent<UIInventory>();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            var slotTransform = _rectTransform.parent;
            slotTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
            _isDragged = true;
            //Inventory.Deselect();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            // TODO: figure how the hell does it work
            // wow a new one
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        }

        // add lerp here?
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
            _isDragged = false;

            //TEST
            //Inventory.MoveEquipmentSlotsAbove();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isDragged) return;
            OnClickNoDrag();
        }

        protected virtual void OnClickNoDrag()
        {
        }
    }

    public class UIDice : DraggableElement
    {
#pragma warning disable 0649
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI amountText;
#pragma warning restore 0649

        private BoxCollider2D _uiCollider;

        public bool showAmountAsText = false;
        public UIDice Item { get; private set; }

        private bool _colliderInit = false;

        private void InitCollider()
        {
            if (_colliderInit) return;
            _uiCollider = GetComponent<BoxCollider2D>();
            ResizeCollider();
            _colliderInit = true;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            //if (!Item.Data.IsDraggable) return;
            base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            //if (!Item.Data.IsDraggable) return;
            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            //if (!Item.Data.IsDraggable) return;
            base.OnEndDrag(eventData);
        }

        public void ResizeCollider(bool useFixedSize = false, int x = -1, int y = -1)
        {
            if (_uiCollider == null) return;

            _uiCollider.size =
                !useFixedSize ? icon.gameObject.GetComponent<RectTransform>().sizeDelta : new Vector2(x, y);
        }

        private void Awake()
        {
            InitCollider();
        }

        protected override void OnClickNoDrag()
        {
            // A fix for RayCasts somehow going through other UI elements
            //if (!EventSystem.current.IsPointerOverGameObject()) return;
            //if (EventSystem.current.currentSelectedGameObject != null) return;

            // Select via inventory
            //Inventory.SelectItem(this);
        }

        /*public void Refresh(InventorySlot slot)
        {
            InitCollider();
            if (slot.IsEmpty)
            {
                HideSlot();
                return;
            }

            // if there IS something
            // UNHIDE THE SLOT
            Item = slot.Item;
            icon.sprite = Item.Data.SpriteIcon;
            icon.gameObject.SetActive(true);
            _uiCollider.enabled = true;

            var textAmountEnabled = slot.Amount > 1 && showAmountAsText;
            amountText.gameObject.SetActive(textAmountEnabled);

            if (textAmountEnabled)
                amountText.text = slot.Amount.ToString();
        }*/

        private void HideSlot()
        {
            amountText.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);

            if (_uiCollider == null) return;
            _uiCollider.enabled = false;
        }
    }
}
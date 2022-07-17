using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.DragDrop
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Canvas canvas;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        public bool maskAlpha = false;

        [ShowInInspector, ReadOnly, FoldoutGroup("Debug Info")]
        private bool _isInSlot;

        [ShowInInspector, ReadOnly, FoldoutGroup("Debug Info")]
        private GameObject _currentSlot;

        [Button]
        public void SetToSlot(GameObject slot)
        {
            transform.position = slot.transform.position;
            _currentSlot = slot;
            _isInSlot = true;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            if (maskAlpha)
                GetComponent<Image>().alphaHitTestMinimumThreshold = 0.95f;
        }

        private Vector3 _startPos;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPos = transform.position;

            _isInSlot = false;
            // will it work?
            if (_currentSlot)
                _currentSlot.GetComponent<ItemSlot>()?.NullDice();

            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //_rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            AltOnDrag(eventData);
        }

        private void AltOnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, eventData.position,
                canvas.worldCamera, out var pos);
            transform.position = canvas.transform.TransformPoint(pos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isInSlot)
            {
                // means that we released it outside of slot
                transform.position = _startPos;
                _isInSlot = true;
            }

            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
    }
}
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class HoldButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _pointerDown;
        private float _pointerDownTime;

        public float requiredHoldTime = 1f;
        public UnityEvent onLongClick;
        public bool debugEnabled = false;
        public CanvasGroup cg;

        [SerializeField, ShowInInspector, Required]
        private Image fillImage;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (debugEnabled) Debug.Log("OnPointerDown");
            _pointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (debugEnabled) Debug.Log("OnPointerUp");
            Reset();
        }

        private void FixedUpdate()
        {
            if (_pointerDown)
            {
                _pointerDownTime += Time.fixedDeltaTime;
                if (_pointerDownTime > requiredHoldTime)
                {
                    onLongClick?.Invoke();
                    Reset();
                }
            }

            fillImage.fillAmount = _pointerDownTime / requiredHoldTime;
        }

        private void Reset()
        {
            _pointerDown = false;
            _pointerDownTime = 0;
            fillImage.fillAmount = _pointerDownTime / requiredHoldTime;
        }

        [Button]
        public void HideButton()
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
        }

        [Button]
        public void ShowButton()
        {
            cg.alpha = 1;
            cg.blocksRaycasts = true;
        }
    }
}
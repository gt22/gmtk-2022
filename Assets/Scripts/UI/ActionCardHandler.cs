using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class ActionCardHandler : MonoBehaviour
    {
        [Required, BoxGroup("References")] public GameObject uiCardPrefab;
        [Required, BoxGroup("References")] public GameObject uiCardPlacement;
        [Required, BoxGroup("References")] public Canvas parentCanvas;

        [ShowInInspector, ReadOnly, FoldoutGroup("Debug")]
        private GameObject _activeCard;

        [ShowInInspector, ReadOnly, FoldoutGroup("Debug")]
        private bool _cardSpawned = false;

        private CanvasGroup _cardCg;
        private BoxCollider2D _cardCollider;

        private void SpawnCard()
        {
            _activeCard = Instantiate(uiCardPrefab, uiCardPlacement.transform.position, Quaternion.identity);
            _activeCard.transform.SetParent(parentCanvas.gameObject.transform, false);
            _activeCard.transform.position = uiCardPlacement.transform.position;
            //MoveUIObject(gameObject, _activeCard.GetComponent<RectTransform>());

            _cardSpawned = true;
            _cardCg = _activeCard.GetComponent<CanvasGroup>();
            _cardCollider = _activeCard.GetComponent<BoxCollider2D>();
            Show();
        }


        [Button]
        public void Show()
        {
            if (!_cardSpawned)
            {
                SpawnCard();
                return;
            }

            _cardCg.alpha = 1;
            _cardCg.blocksRaycasts = true;
            _cardCollider.enabled = true;
        }

        [Button]
        public void Hide()
        {
            if (_activeCard == null || _cardCg == null) return;

            _cardCg.alpha = 0;
            _cardCg.blocksRaycasts = false;
            _cardCollider.enabled = false;
        }

        //TODO why doesn't work
        private void MoveUIObject(GameObject worldObject, RectTransform uiElement)
        {
            var canvasRect = parentCanvas.GetComponent<RectTransform>();
            var viewportPosition = Camera.main.WorldToViewportPoint(worldObject.transform.position);
            var worldObjectScreenPosition = new Vector2(
                ((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

            uiElement.anchoredPosition = worldObjectScreenPosition;
        }
    }
}
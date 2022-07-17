using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class ActionCardHandler : MonoBehaviour
    {
        [Required, BoxGroup("ActionCard")] public GameObject uiCardPrefab;
        [Required, BoxGroup("ActionCard")] public GameObject uiSituationCardPrefab;

        [Required, BoxGroup("SituationCard")] public GameObject uiCardPlacement;
        [Required, BoxGroup("SituationCard")] public GameObject uiSituationCardPlacement;

        [Required, BoxGroup("References")] public Canvas parentCanvas;

        [FoldoutGroup("Card Content")] public string actionTitle;

        [FoldoutGroup("Card Content"), TextArea]
        public string actionDescription;

        [FoldoutGroup("Card Content")] public string situationTitle;

        [FoldoutGroup("Card Content"), TextArea]
        public string situationDescription;

        [ShowInInspector, ReadOnly, FoldoutGroup("Debug")]
        private GameObject _activeActionCard;

        [ShowInInspector, ReadOnly, FoldoutGroup("Debug")]
        private GameObject _activeSituationCard;

        private ActionCardScript _cardScript;
        private ActionCardScript _situationCardScript;

        [ShowInInspector, ReadOnly, FoldoutGroup("Debug")]
        private bool _cardsSpawned = false;

        private CanvasGroup _cardCg;
        private CanvasGroup _situationCardCg;

        private void SpawnCard()
        {
            // action card
            _activeActionCard = Instantiate(uiCardPrefab, uiCardPlacement.transform.position, Quaternion.identity);
            _activeActionCard.transform.SetParent(parentCanvas.gameObject.transform, false);
            _activeActionCard.transform.position = uiCardPlacement.transform.position;
            _cardScript = _activeActionCard.GetComponent<ActionCardScript>();
            _cardScript.SetTitle(actionTitle);
            _cardScript.SetDescription(actionDescription);

            // situation card
            _activeSituationCard = Instantiate(uiSituationCardPrefab, uiSituationCardPlacement.transform.position,
                Quaternion.identity);
            _activeSituationCard.transform.SetParent(parentCanvas.gameObject.transform, false);
            _activeSituationCard.transform.position = uiSituationCardPlacement.transform.position;
            _situationCardScript = _activeSituationCard.GetComponent<ActionCardScript>();
            _situationCardScript.SetTitle(situationTitle);
            _situationCardScript.SetDescription(situationDescription);

            _cardsSpawned = true;
            _cardCg = _activeActionCard.GetComponent<CanvasGroup>();
            _situationCardCg = _activeSituationCard.GetComponent<CanvasGroup>();

            _activeActionCard.transform.SetAsFirstSibling();
            _activeSituationCard.transform.SetAsFirstSibling();
            ShowCards();
        }


        [Button]
        public void ShowCards()
        {
            if (!_cardsSpawned)
            {
                SpawnCard();
                return;
            }

            _cardScript.Show();
            _situationCardScript.Show();
        }

        [Button]
        public void HideCards()
        {
            if (_activeActionCard == null || _cardCg == null || _situationCardCg == null) return;

            _cardScript.Hide();
            _situationCardScript.Hide();

            //_activeCard.GetComponentInChildren<ItemSlot>().currentDice.GetComponent<CanvasGroup>().alpha = 0;
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
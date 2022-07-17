using TMPro;
using UI.DragDrop;
using UnityEngine;

namespace UI
{
    public class ActionCardScript : MonoBehaviour
    {
        private CanvasGroup _cardCg;
        private CanvasGroup _diceCanvasGroup;
        private BoxCollider2D _cardCollider;
        private ItemSlot _diceSlot;

        public TextMeshProUGUI titleText;
        public TextMeshProUGUI descriptionText;

        public void SetTitle(string s) => titleText.text = s;
        public void SetDescription(string s) => descriptionText.text = s;

        private void Awake()
        {
            _cardCg = GetComponent<CanvasGroup>();
            _cardCollider = GetComponent<BoxCollider2D>();
            _diceSlot = GetComponentInChildren<ItemSlot>();
        }

        public void Show()
        {
            _cardCg.alpha = 1;
            _cardCg.blocksRaycasts = true;
            _cardCollider.enabled = true;
            _diceSlot.ShowDice();
        }

        public void Hide()
        {
            _cardCg.alpha = 0;
            _cardCg.blocksRaycasts = false;
            _cardCollider.enabled = false;
            _diceSlot.HideDice();
        }
    }
}
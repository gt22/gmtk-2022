using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.DragDrop
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        public GameObject currentDice = null;

        public bool HasDice => currentDice != null;

        public void HideDice()
        {
            if (currentDice == null) return;
            var cg = currentDice.GetComponent<CanvasGroup>();

            cg.alpha = 0;
            cg.blocksRaycasts = false;
        }

        public void ShowDice()
        {
            if (currentDice == null) return;
            var cg = currentDice.GetComponent<CanvasGroup>();

            cg.alpha = 1;
            cg.blocksRaycasts = true;
        }

        public void NullDice()
        {
            currentDice = null;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;

            var dd = eventData.pointerDrag.gameObject.GetComponent<DragDrop>();
            if (dd == null) return;

            currentDice = eventData.pointerDrag.gameObject;
            dd.SetToSlot(gameObject);
        }
    }
}
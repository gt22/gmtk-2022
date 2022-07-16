using Resources;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResourceUpdater : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField]
        public TextMeshProUGUI manpowerText;

        [FoldoutGroup("References"), SerializeField]
        public TextMeshProUGUI materialsText;

        [FoldoutGroup("References"), SerializeField]
        public TextMeshProUGUI suppliesText;

        public TextMeshProUGUI testText;

        private void OnEnable()
        {
            ResourceManager.OnUpdateResources += UpdateText;
        }

        private void OnDisable()
        {
            ResourceManager.OnUpdateResources -= UpdateText;
        }

        private void UpdateText(ResourceStack s)
        {
            manpowerText.text = s.Manpower.ToString();
            materialsText.text = s.Materials.ToString();
            suppliesText.text = s.Supplies.ToString();
        }
    }
}
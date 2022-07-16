using Resources;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResourceUpdater : MonoBehaviour
    {
        [FoldoutGroup("References")] public TextMeshProUGUI manpowerText;
        [FoldoutGroup("References")] public TextMeshProUGUI materialsText;
        [FoldoutGroup("References")] public TextMeshProUGUI suppliesText;

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
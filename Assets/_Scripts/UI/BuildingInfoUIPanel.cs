using JustGame.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class BuildingInfoUIPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private TextMeshProUGUI m_buildingName;
        [SerializeField] private Image m_buildingShieldIcon;
        [SerializeField] private Sprite[] m_armorTypeSprite;

        private void OnEnable()
        {
            m_canvasGroup.alpha = 0;
        }

        public void DisplayInfo(string buildingName, ArmorType armorType)
        {
            m_canvasGroup.alpha = 1;
            m_buildingName.text = buildingName;
            m_buildingShieldIcon.sprite = m_armorTypeSprite[(int)armorType];
        }

        public void HideInfo()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}

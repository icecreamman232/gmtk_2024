using JustGame.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class BuildingInfoUIPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private TextMeshProUGUI m_buildingName;
        [SerializeField] private Image m_buildingArmorIcon;
        [SerializeField] private Image m_buildingAtkIcon;
        [SerializeField] private Sprite[] m_armorTypeSprite;
        [SerializeField] private Sprite[] m_atkTypeSprite;

        private void OnEnable()
        {
            m_canvasGroup.alpha = 0;
        }

        public void DisplayInfo(string buildingName, OffenseData offenseData)
        {
            m_canvasGroup.alpha = 1;
            m_buildingName.text = buildingName;
            m_buildingAtkIcon.gameObject.SetActive(true);
            m_buildingAtkIcon.sprite = m_atkTypeSprite[(int)offenseData.AttackType];
            m_buildingArmorIcon.sprite = m_armorTypeSprite[(int)offenseData.ArmorType];
        }
        
        public void DisplayInfo(string buildingName, DefenseData defenseData)
        {
            m_canvasGroup.alpha = 1;
            m_buildingName.text = buildingName;
            m_buildingAtkIcon.gameObject.SetActive(false);
            m_buildingArmorIcon.sprite = m_armorTypeSprite[(int)defenseData.ArmorType];
        }

        public void HideInfo()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}

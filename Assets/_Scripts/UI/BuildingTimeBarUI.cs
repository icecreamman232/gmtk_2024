using System;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class BuildingTimeBarUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_fillBar;

        private void OnEnable()
        {
            m_canvasGroup.alpha = 0;
        }

        public void UpdateFillBar(float value)
        {
            m_fillBar.fillAmount = value;

            if (m_fillBar.fillAmount is > 0 and < 1)
            {
                m_canvasGroup.alpha = 1;
            }

            if (m_fillBar.fillAmount >= 1)
            {
                m_canvasGroup.alpha = 0;
            }
        }
    }
}

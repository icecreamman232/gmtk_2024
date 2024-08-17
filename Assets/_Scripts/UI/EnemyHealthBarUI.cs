using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class EnemyHealthBarUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_healthBar;
        private void OnEnable()
        {
            m_canvasGroup.alpha = 0;
        }

        public void UpdateHealthBar(float value)
        {
            if (value > 0)
            {
                m_canvasGroup.alpha = 1;
            }
            m_healthBar.fillAmount = value;
        }
    }
}


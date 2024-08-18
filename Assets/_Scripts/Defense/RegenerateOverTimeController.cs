using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public class RegenerateOverTimeController : MonoBehaviour
    {
        [SerializeField] private BuildingHealth m_health;
        [SerializeField] private float m_frequentTimeToRegen;
        [SerializeField] private float m_healthToRegen;
        [SerializeField] private Animator m_animator;

        private int m_bool_healing_animParams = Animator.StringToHash("Bool_Healing");
        private float m_timer;
        
        private void Update()
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_frequentTimeToRegen)
            {
                m_timer = 0;
                Healing();
            }
            
            if (m_health != null)
            {
                m_animator.SetBool(m_bool_healing_animParams, m_health.CurrentHealth < m_health.MaxHealth);
            }
        }

        private void Healing()
        {
            m_health.CustomHealing(m_healthToRegen);
        }
    }
}


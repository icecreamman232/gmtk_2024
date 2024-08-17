using System.Collections;
using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public class BuildingHealth : MonoBehaviour, Damageable
    {
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;

        private bool m_isInvulnerable;

        public void Initialize(float maxHealth)
        {
            m_maxHealth = maxHealth;
            m_curHealth = maxHealth;
        }

        public void TakeDamage(float damage, float invulnerableDuration, GameObject instigator)
        {
            if (m_isInvulnerable) return;

            if (m_curHealth <= 0) return;

            m_curHealth -= damage;

            if (m_curHealth <= 0)
            {
                Kill();
            }
            else
            {
                StartCoroutine(OnInvulnerable(invulnerableDuration));
            }
        }

        private void Kill()
        {
            gameObject.SetActive(false);
        }
        
        private IEnumerator OnInvulnerable(float duration)
        {
            m_isInvulnerable = true;
            yield return new WaitForSeconds(duration);
            m_isInvulnerable = false;
        }
    }
}

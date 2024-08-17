using System;
using System.Collections;
using JustGame.Scripts.Defense;
using JustGame.Scripts.Managers;
using JustGame.Scripts.UI;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, Damageable
    {
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;
        [SerializeField] private EnemyHealthBarUI m_healthBar;

        private bool m_isInvulnerable;

        //Placeholder
        private void Start()
        {
            Initialize(m_maxHealth);
        }

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
            m_healthBar.UpdateHealthBar(MathHelpers.Remap(m_curHealth,0,m_maxHealth,0,1));
            
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


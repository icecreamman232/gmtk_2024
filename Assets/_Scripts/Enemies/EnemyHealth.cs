using System;
using System.Collections;
using JustGame.Scripts.Defense;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.UI;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, Damageable
    {
        [SerializeField] private float m_scaleLevelPercent;
        [SerializeField] private EnemyMovement m_movement;
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;
        [SerializeField] private HealthBarUI m_healthBar;
        [SerializeField] private EnemyDeathEvent m_enemyDeathEvent;
        [SerializeField] private bool m_isElite;

        private bool m_isInvulnerable;

        public bool IsElite => m_isElite;
        public Action OnDeath;

        //Placeholder
        private void Start()
        {
            Initialize(m_maxHealth);
        }

        public void SetHealthBasedOnLevel(int level)
        {
            if (level <= 1) return;
            m_maxHealth += m_maxHealth * (level * m_scaleLevelPercent / 100);
            m_curHealth = m_maxHealth;
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

        public void ManualKill()
        {
            Kill();
        }
        

        private void Kill()
        {
            m_enemyDeathEvent.Raise(this);
            OnDeath?.Invoke();
            m_movement.StopMoving();
            Destroy(gameObject);
        }
        
        private IEnumerator OnInvulnerable(float duration)
        {
            m_isInvulnerable = true;
            yield return new WaitForSeconds(duration);
            m_isInvulnerable = false;
        }
    }
}


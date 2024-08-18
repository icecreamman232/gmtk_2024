using System;
using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.Defense;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.UI;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, Damageable
    {
        [Header("Params")] 
        [SerializeField] private ArmorType m_armorType;
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;
        [SerializeField] private bool m_isElite;
        [Header("Scale with Level")]
        [SerializeField] private float m_scaleLevelPercent;

        [Header("Refs")] 
        [SerializeField] private DamageArmorTable m_damageArmorTable;
        [SerializeField] private EnemyMovement m_movement;
        [SerializeField] private HealthBarUI m_healthBar;
        [SerializeField] private EnemyDeathEvent m_enemyDeathEvent;

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

        public void TakeDamage(AttackType atkType, float damage, float invulnerableDuration, GameObject instigator)
        {
            if (m_isInvulnerable) return;

            if (m_curHealth <= 0) return;
            
            m_curHealth -= CalculateFinalDamage(damage,atkType, m_armorType);
            
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

        private float CalculateFinalDamage(float damage,AttackType atkType, ArmorType armorType)
        {
            var finalDamage = m_damageArmorTable.GetFinalDamage(damage, atkType, armorType);
            return finalDamage;
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


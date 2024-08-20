using System;
using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.UI;
using JustGame.Scripts.World;
using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public class BuildingHealth : MonoBehaviour, Damageable
    {
        [SerializeField] private BuildingData m_buildingData;
        [SerializeField] private DamageArmorTable m_damageArmorTable;
        [SerializeField] private ArmorType m_armorType;
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;
        [SerializeField] private HealthBarUI m_healthBar;

        public Action OnDeath;

        public float CurrentHealth => m_curHealth;
        public float MaxHealth => m_maxHealth;
        
        private bool m_isInvulnerable;

        public void Initialize(float maxHealth)
        {
            m_maxHealth = maxHealth;
            m_curHealth = maxHealth;
        }

        public void CustomHealing(float healAmount)
        {
            m_curHealth += healAmount;
            if (m_curHealth >= m_maxHealth)
            {
                m_curHealth = m_maxHealth;
            }
        }

        private ArmorType GetArmorType()
        {
            return m_buildingData.DefenseData != null 
                ? m_buildingData.DefenseData.ArmorType 
                : m_buildingData.OffenseData.ArmorType;
        }

        public void TakeDamage(AttackType atkType, float damage, float invulnerableDuration, GameObject instigator)
        {
            if (m_isInvulnerable) return;

            if (m_curHealth <= 0) return;

            m_curHealth -= CalculateFinalDamage(damage, atkType, GetArmorType());
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

        private void Kill()
        {
            PathFinding.Instance.RemoveObstacle(transform.position);
            OnDeath?.Invoke();
            Destroy(this.gameObject);
        }
        
        private IEnumerator OnInvulnerable(float duration)
        {
            m_isInvulnerable = true;
            yield return new WaitForSeconds(duration);
            m_isInvulnerable = false;
        }
    }
}

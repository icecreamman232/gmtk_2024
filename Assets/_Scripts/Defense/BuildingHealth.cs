using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.UI;
using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public class BuildingHealth : MonoBehaviour, Damageable
    {
        [SerializeField] private DamageArmorTable m_damageArmorTable;
        [SerializeField] private ArmorType m_armorType;
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;
        [SerializeField] private HealthBarUI m_healthBar;

        private bool m_isInvulnerable;

        public void Initialize(float maxHealth)
        {
            m_maxHealth = maxHealth;
            m_curHealth = maxHealth;
        }

        public void TakeDamage(AttackType atkType, float damage, float invulnerableDuration, GameObject instigator)
        {
            if (m_isInvulnerable) return;

            if (m_curHealth <= 0) return;

            m_curHealth -= CalculateFinalDamage(damage, atkType, m_armorType);
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

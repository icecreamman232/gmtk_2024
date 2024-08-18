using System;
using System.Collections;
using JustGame.Scripts.Damage;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] protected DamageOnTouch m_damageArea;
        [SerializeField] protected float m_atkDuration;
        [SerializeField] protected float m_delayBetween2Hit;

        protected bool m_isDelay;
        

        public virtual void StartAttack()
        {
            if (m_isDelay) return;
            StartCoroutine(OnAttack());
        }

        protected virtual IEnumerator OnAttack()
        {
            m_damageArea.gameObject.SetActive(true);
            StartCoroutine(OnDelayAttack());
            yield return new WaitForSeconds(m_atkDuration);
            m_damageArea.gameObject.SetActive(false);
        }

        public virtual void StopAttack()
        {
            StopAllCoroutines();
            m_isDelay = false;
            m_damageArea.gameObject.SetActive(false);
        }

        protected virtual IEnumerator OnDelayAttack()
        {
            m_isDelay = true;
            yield return new WaitForSeconds(m_delayBetween2Hit);
            m_isDelay = false;
        }
    }
}


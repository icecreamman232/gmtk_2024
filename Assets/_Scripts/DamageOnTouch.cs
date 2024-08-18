using System;
using JustGame.Scripts.Defense;
using JustGame.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Damage
{
    public class DamageOnTouch : MonoBehaviour
    {
        [SerializeField] private float m_invulnerableDuration;
        [SerializeField] private float m_minDamage;
        [SerializeField] private float m_maxDamage;
        [SerializeField] private LayerMask m_targetMask;
        [SerializeField] private Collider2D m_collider2D;

        public Collider2D Collider2D => m_collider2D == null ? GetComponent<Collider2D>() : m_collider2D;
        public Action<Collider2D> OnHit;
        
        private float GetDamage()
        {
            return Random.Range(m_minDamage, m_maxDamage);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask))
            {
                Hit(other);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask))
            {
                Hit(other);
            }
        }

        private void Hit(Collider2D other)
        {
            var damage = GetDamage();
            
            var damageComponent = other.gameObject.GetComponentInParent<Damageable>();
            
            if (damageComponent != null)
            {
                //Debug.Log("Deal Damage");
                OnHit?.Invoke(other);
                damageComponent.TakeDamage(damage,m_invulnerableDuration,this.transform.parent.gameObject);
            }
        }
    }
}


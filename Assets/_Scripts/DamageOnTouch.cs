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

        private void Hit(Collider2D other)
        {
            var damage = GetDamage();
            var damageComponent = other.gameObject.GetComponentInParent<Damageable>();
            if (damageComponent != null)
            {
                damageComponent.TakeDamage(damage,m_invulnerableDuration,this.transform.parent.gameObject);
            }
        }
    }
}


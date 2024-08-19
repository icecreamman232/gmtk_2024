using System.Collections;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Damage
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] protected ObjectPooler m_projectilePooler;
        [SerializeField] protected float m_delayBetween2Shot;
        [SerializeField] protected Transform m_shootPivot;
        [SerializeField] protected Vector2 m_target;

        protected bool m_isDelay;

        protected virtual IEnumerator OnDelayShoot()
        {
            m_isDelay = true;
            yield return new WaitForSeconds(m_delayBetween2Shot);
            m_isDelay = false;
        }
        
        public virtual void Shoot(Vector2 position)
        {
            if (m_isDelay) return;
            
            var projectileGO = m_projectilePooler.GetPooledGameObject();
            var projectile = projectileGO.GetComponent<Projectile>();
            projectile.transform.position = m_shootPivot.position;
            projectile.transform.rotation = Quaternion.identity;
            if (projectile != null)
            {
                projectile.SetMoveTo(position);
                StartCoroutine(OnDelayShoot());
            }
        }
    }
}


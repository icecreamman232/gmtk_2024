using UnityEngine;

namespace JustGame.Scripts.Damage
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected float m_offsetRot;
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected float m_distanceToStop;
        
        protected Vector2 m_targetPosition;

        protected bool m_shouldMove;

        protected virtual void Update()
        {
            if (!m_shouldMove) return;
            UpdateMoment();
        }

        public virtual void SetMoveTo(Vector2 target)
        {
            m_targetPosition = target;
            var direction = (target - (Vector2)transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + m_offsetRot;
            transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
            m_shouldMove = true;
        }

        public virtual void StopMoving()
        {
            m_shouldMove = false;
        }

        protected virtual void DestroyProjectile()
        {
            gameObject.SetActive(false);
        }

        protected virtual void UpdateMoment()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, m_targetPosition, Time.deltaTime * m_moveSpeed);
            if(Vector2.Distance(transform.position,m_targetPosition) <= m_distanceToStop)
            {
                StopMoving();
                DestroyProjectile();
            }
        }
    }
}


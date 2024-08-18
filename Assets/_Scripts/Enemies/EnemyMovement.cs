using System;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float m_scaleSpeed;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Transform m_target;
        [SerializeField] private Vector3 m_offsetRaycast;
        [SerializeField] private float m_distanceToCheckObstacle;
        [SerializeField] private LayerMask m_obstacleMask;
        
        private EnemyController m_controller;
        private RaycastHit2D m_hit2D;
        private Vector2 m_direction;

        private bool m_shouldMove;

        public Action<Collider2D> OnHitObstacle;
        
        public void Initialize(EnemyController enemyController, int level)
        {
            m_controller = enemyController;
            if (level > 1)
            {
                m_moveSpeed += m_moveSpeed * level * m_scaleSpeed / 100;
            }
        }

        public void MoveTo(Transform targetTransform)
        {
            m_target = targetTransform;
            m_shouldMove = true;
        }

        public void StopMoving()
        {
            m_shouldMove = false;
        }

        private void Update()
        {
            if (m_target == null) return;
            
            if (CheckObstacle())
            {
                StopMoving();
            }
            else
            {
                //Resume moving to target if obstacle got destroyed
                if (!m_shouldMove && m_target!=null)
                {
                    MoveTo(m_target);
                }
            }
            
            if (!m_shouldMove) return;
            UpdateMovement();
        }

        private bool CheckObstacle()
        {
            m_direction = (m_target.position - transform.position).normalized;
            m_hit2D = Physics2D.Raycast(transform.position + m_offsetRaycast, Vector2.left, m_distanceToCheckObstacle, m_obstacleMask);
            if (m_hit2D.collider != null)
            {
                OnHitObstacle?.Invoke(m_hit2D.collider);
                return true;
            }
            return false;
        }
        
        private void UpdateMovement()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, m_target.position, Time.deltaTime * m_moveSpeed);
            if (Vector2.Distance(transform.position, m_target.position) <= 1)
            {
                m_controller.RequestNextPoint();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + m_offsetRaycast,transform.position + Vector3.left * m_distanceToCheckObstacle + m_offsetRaycast);
        }
    }
}


using System;
using JustGame.Scripts.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float m_scaleSpeed;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_target;
        [SerializeField] private Vector3 m_offsetRaycast;
        [SerializeField] private float m_distanceToCheckObstacle;
        [SerializeField] private LayerMask m_obstacleMask;
        [SerializeField] private Vector2 m_randomX;
        [SerializeField] private Vector2 m_randomY;
        
        private EnemyController m_controller;
        private RaycastHit2D m_hit2D;
        private Vector2 m_direction;
        private Vector2 m_offset;

        private bool m_shouldMove;

        public Action<Collider2D> OnHitObstacle;

        private void OnEnable()
        {
            PathFinding.Instance.OnAddObstacle += OnAddObstacleInWorld;
        }

        private void OnDisable()
        {
            PathFinding.Instance.OnAddObstacle -= OnRemoveObstacleInWorld;
        }

        public void Initialize(EnemyController enemyController, int level)
        {
            m_controller = enemyController;
            if (level > 1)
            {
                m_moveSpeed += m_moveSpeed * level * m_scaleSpeed / 100;
            }

            m_offset = Vector2.zero;
            m_offset.x += Random.Range(-m_randomX.x, m_randomX.x);
            m_offset.y += Random.Range(-m_randomX.y, m_randomX.y);
        }

        public void MoveTo(Vector2 targetPos)
        {
            m_target = targetPos + m_offset;
            m_shouldMove = true;
        }

        public void StopMoving()
        {
            m_shouldMove = false;
        }

        private void OnAddObstacleInWorld()
        {
            if (m_controller == null) return;
            m_controller.FindNewPath();
            MoveTo(m_controller.GetNextTarget());
        }

        private void OnRemoveObstacleInWorld()
        {
            if (m_controller == null) return;
            m_controller.FindNewPath();
            MoveTo(m_controller.GetNextTarget());
        }

        private void Update()
        {
            if (m_target == null) return;
            
            // if (CheckObstacle())
            // {
            //     m_controller.FindNewPath();
            //     MoveTo(m_controller.GetNextTarget());
            // }
            // else
            // {
            //     //Resume moving to target if obstacle got destroyed
            //     if (!m_shouldMove && m_target!=null)
            //     {
            //         m_controller.FindNewPath();
            //         MoveTo(m_controller.GetNextTarget());
            //     }
            // }
            
            if (!m_shouldMove) return;
            UpdateMovement();
        }

        private bool CheckObstacle()
        {
            m_direction = (m_target - (Vector2)transform.position).normalized;
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
                Vector2.MoveTowards(transform.position, m_target, Time.deltaTime * m_moveSpeed);
            if (Vector2.Distance(transform.position, m_target) <= 0.05f)
            {
                MoveTo(m_controller.GetNextTarget());
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + m_offsetRaycast,transform.position + Vector3.left * m_distanceToCheckObstacle + m_offsetRaycast);
        }
    }
}


using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Transform m_target;

        private EnemyController m_controller;
        
        public void Initialize(EnemyController enemyController)
        {
            m_controller = enemyController;
        }

        public void MoveTo(Transform targetTransform)
        {
            m_target = targetTransform;
        }

        public void StopMoving()
        {
            m_target = null;
        }

        private void Update()
        {
            if (m_target == null) return;

            UpdateMovement();
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
    }
}


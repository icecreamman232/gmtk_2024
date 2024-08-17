using System;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_movingDirection;
        [SerializeField] private Vector2 m_worldBoundary;

        private Vector2 m_curPos;
        private void Update()
        {
            HandleInput();
            UpdateMovement();
        }

        private void HandleInput()
        {
            m_movingDirection = Vector2.zero;
            
            if (Input.GetKey(KeyCode.A))
            {
                m_movingDirection.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_movingDirection.x = 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                m_movingDirection.y = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_movingDirection.y = -1;
            }
        }

        private void UpdateMovement()
        {
            transform.Translate(m_movingDirection * (Time.deltaTime * m_moveSpeed));
            m_curPos = transform.position;
            if (m_curPos.x >= m_worldBoundary.x/2)
            {
                m_curPos.x = m_worldBoundary.x/2;
            }
            if (m_curPos.x <= -m_worldBoundary.x/2)
            {
                m_curPos.x = -m_worldBoundary.x/2;
            }
            if (m_curPos.y >= m_worldBoundary.y/2)
            {
                m_curPos.y = m_worldBoundary.y/2;
            }
            if (m_curPos.y <= -m_worldBoundary.y/2)
            {
                m_curPos.y = -m_worldBoundary.y/2;
            }
            transform.position = m_curPos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Vector3.zero,m_worldBoundary);
        }
    }
}


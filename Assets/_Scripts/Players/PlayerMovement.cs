using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_movingDirection;
        [SerializeField] private Vector2 m_worldBoundary;
        [SerializeField] private Camera m_mainCamera;
        [SerializeField] private Vector3Event m_buildingPlacePosEvent;

        private Vector2 m_curPos;
        private Vector2 m_destinationPos;

        private void OnEnable()
        {
            m_buildingPlacePosEvent.AddListener(MovePlayerToPosition);
        }
        private void OnDisable()
        {
            m_buildingPlacePosEvent.RemoveListener(MovePlayerToPosition);
        }

        private void Update()
        {
            HandleInput();
            UpdateMovement();
        }

        private void HandleInput()
        {
            //Right click
            if (Input.GetMouseButtonDown(1))
            {
                MovePlayerToPosition();
            }
        }

        private void MovePlayerToPosition()
        {
            m_destinationPos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        private void MovePlayerToPosition(Vector3 newPos)
        {
            m_destinationPos = newPos;
        }

        private void UpdateMovement()
        {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                m_destinationPos, 
                Time.deltaTime * m_moveSpeed);
            CheckWorldBoundary();
        }

        private void CheckWorldBoundary()
        {
            m_curPos = transform.position;
            if (m_curPos.x >= m_worldBoundary.x/2)
            {
                m_curPos.x = m_worldBoundary.x/2;
                m_destinationPos = m_curPos;
            }
            if (m_curPos.x <= -m_worldBoundary.x/2)
            {
                m_curPos.x = -m_worldBoundary.x/2;
                m_destinationPos = m_curPos;
            }
            if (m_curPos.y >= m_worldBoundary.y/2)
            {
                m_curPos.y = m_worldBoundary.y/2;
                m_destinationPos = m_curPos;
            }
            if (m_curPos.y <= -m_worldBoundary.y/2)
            {
                m_curPos.y = -m_worldBoundary.y/2;
                m_destinationPos = m_curPos;
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


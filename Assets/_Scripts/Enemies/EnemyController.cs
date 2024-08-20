using System.Collections.Generic;
using JustGame.Scripts.World;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected EnemyMovement m_movement;
        [SerializeField] protected EnemyHealth m_health;
    
        protected PointController m_targetPoint;
        private List<Node> m_path;
        private int m_curTargetNodeIndex;
    
        public virtual void Initialize(PointController startPoint, int level)
        {
            m_path = new List<Node>();
            m_targetPoint = startPoint.NextPoint;
            m_health.SetHealthBasedOnLevel(level);
            m_curTargetNodeIndex = 0;
            m_movement.Initialize(this,level);
            
            if (FindNewPath())
            {
                m_movement.MoveTo(GetNextTarget());
            }
            m_movement.OnHitObstacle += OnHitObstacle;
        }

        public bool FindNewPath()
        {
            m_path = PathFinding.Instance.FindPath(transform.position, m_targetPoint.transform.position);
            if (m_path == null)
            {
                return false;
            }
            
            return m_path.Count > 0;
        }

        public Vector2 GetNextTarget()
        {
            m_curTargetNodeIndex++;
            if (m_curTargetNodeIndex >= m_path.Count)
            {
                return m_path[^1].WorldPosition;
            }
            return m_path[m_curTargetNodeIndex].WorldPosition;
        }

        protected virtual void OnHitObstacle(Collider2D obstacle)
        {
        
        }
    
        // public virtual void RequestNextPoint()
        // {
        //     //Last point so we stop here
        //     if (m_targetPoint.NextPoint == null)
        //     {
        //         m_movement.StopMoving();
        //         gameObject.SetActive(false);
        //         Destroy(gameObject);
        //     }
        //     else
        //     {
        //         //Move to next point
        //         m_targetPoint = m_targetPoint.NextPoint;
        //         m_movement.MoveTo(m_targetPoint.transform);
        //     }
        // }

        private void OnDestroy()
        {
            m_movement.OnHitObstacle -= OnHitObstacle;
        }
    }
}

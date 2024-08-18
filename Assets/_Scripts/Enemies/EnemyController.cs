using JustGame.Scripts.World;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected EnemyMovement m_movement;
    
        protected PointController m_targetPoint;
    
        public virtual void Initialize(PointController startPoint)
        {
            m_targetPoint = startPoint.NextPoint;
            m_movement.Initialize(this);
            m_movement.MoveTo(m_targetPoint.transform);
            m_movement.OnHitObstacle += OnHitObstacle;
        }

        protected virtual void OnHitObstacle(Collider2D obstacle)
        {
        
        }
    
        public virtual void RequestNextPoint()
        {
            //Last point so we stop here
            if (m_targetPoint.NextPoint == null)
            {
                m_movement.StopMoving();
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                //Move to next point
                m_targetPoint = m_targetPoint.NextPoint;
                m_movement.MoveTo(m_targetPoint.transform);
            }
        }

        private void OnDestroy()
        {
            m_movement.OnHitObstacle -= OnHitObstacle;
        }
    }
}

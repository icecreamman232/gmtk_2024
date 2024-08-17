using JustGame.Scripts.Enemy;
using JustGame.Scripts.World;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyMovement m_movement;

    private PointController m_targetPoint;
    
    public void Initialize(PointController startPoint)
    {
        m_targetPoint = startPoint.NextPoint;
        m_movement.Initialize(this);
        m_movement.MoveTo(m_targetPoint.transform);
    }

    public void RequestNextPoint()
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
}

using JustGame.Scripts.World;
using UnityEngine;

public class WorldPointController : MonoBehaviour
{
    [SerializeField] private PointController[] m_points;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < m_points.Length-1; i++)
        {
            Gizmos.DrawLine(m_points[i].transform.position,m_points[i+1].transform.position);
        }
    }
}

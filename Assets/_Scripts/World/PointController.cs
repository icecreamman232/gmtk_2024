using UnityEngine;

namespace JustGame.Scripts.World
{
    public class PointController : MonoBehaviour
    {
        public PointController NextPoint;

        private void OnDrawGizmosSelected()
        {
            if (NextPoint == null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, NextPoint.transform.position);
        }
    }
}

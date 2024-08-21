using JustGame.Scripts.Managers;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool m_isExit;
    [SerializeField] private LayerMask m_targetMask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask))
        {
            OpenPortal();
        }
    }

    private void OpenPortal()
    {
        
    }
}

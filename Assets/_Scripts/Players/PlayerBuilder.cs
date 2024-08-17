using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerBuilder : MonoBehaviour
    {
        [SerializeField] private LayerMask m_buildableLayerMask;
        private Collider2D m_lastBuildingInteract;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_buildableLayerMask))
            {
                m_lastBuildingInteract = other;
                ActivateBuilding();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other != m_lastBuildingInteract) return;
            DeactivateBuilding();
        }

        private void ActivateBuilding()
        {
            Debug.Log("Activate");
        }

        private void DeactivateBuilding()
        {
            Debug.Log("DeActivate");
        }
    }
}


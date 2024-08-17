using JustGame.Scripts.Defense;
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
                ActivateBuilding(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other != m_lastBuildingInteract) return;
            DeactivateBuilding();
        }

        private void ActivateBuilding(Collider2D other)
        {
            var controller = other.gameObject.GetComponentInParent<BuildingController>();

            if (controller.CurrentState == BuildingState.READY_TO_BUILD)
            {
                controller.SetBuildingState(BuildingState.BUILDING);
            }
        }

        private void DeactivateBuilding()
        {
            var controller = m_lastBuildingInteract.gameObject.GetComponentInParent<BuildingController>();

            if (controller.CurrentState == BuildingState.BUILDING)
            {
                controller.SetBuildingState(BuildingState.READY_TO_BUILD);
            }
        }
    }
}


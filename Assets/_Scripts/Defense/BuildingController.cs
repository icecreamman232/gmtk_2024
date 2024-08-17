using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public enum BuildingState
    {
        IDLE,
        BUILDING,
        DAMAGED,
    }
    
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private BuildingState m_curState;
        [SerializeField] private bool m_isPermit;
        [SerializeField] private Sprite m_buildingIcon;

        public Sprite Icon => m_buildingIcon;
        
        public bool IsPermit
        {
            set => m_isPermit = value;
            get => m_isPermit;
        }

        public void SetBuildingState(BuildingState newState)
        {
            m_curState = newState;
        }
    }
}


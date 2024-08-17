using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private bool m_isPermit;
        [SerializeField] private Sprite m_buildingIcon;

        public Sprite Icon => m_buildingIcon;
        
        public bool IsPermit
        {
            set => m_isPermit = value;
            get => m_isPermit;
        }
    }
}


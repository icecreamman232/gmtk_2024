using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private bool m_isPermit;
        
        public bool IsPermit
        {
            set => m_isPermit = value;
            get => m_isPermit;
        }
    }
}


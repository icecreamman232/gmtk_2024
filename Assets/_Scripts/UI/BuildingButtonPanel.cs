using JustGame.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.UI
{
    public class BuildingButtonPanel : MonoBehaviour
    {
        [SerializeField] private BuildingDataContainer m_buildingContainer;
        [SerializeField] private BuildingButtonController[] m_buttons;

        private void Start()
        {
            foreach (var button in m_buttons)
            {
                button.InitBuildingButton(this);
                button.SetBuildingPrefab(GetRandomBuildingPrefab());
            }
        }

        private GameObject GetRandomBuildingPrefab()
        {
            var randomIndex = Random.Range(0, m_buildingContainer.DefensiveBuilding.Length);
            return m_buildingContainer.DefensiveBuilding[randomIndex];
        }
    }
}


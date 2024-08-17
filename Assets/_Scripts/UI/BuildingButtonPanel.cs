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

        public GameObject GetRandomBuildingPrefab()
        {
            var randomType = Random.Range(0, 2);
            if (randomType == 0)
            {
                var randomIndex = Random.Range(0, m_buildingContainer.DefensiveBuilding.Length);
                return m_buildingContainer.DefensiveBuilding[randomIndex].Prefab;
            }
            else
            {
                var randomIndex = Random.Range(0, m_buildingContainer.OffensiveBuilding.Length);
                return m_buildingContainer.OffensiveBuilding[randomIndex].Prefab;
            }
        }
    }
}


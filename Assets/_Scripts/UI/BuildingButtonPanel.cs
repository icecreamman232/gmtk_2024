using System;
using JustGame.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.UI
{
    public class BuildingButtonPanel : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private int m_forceType;
        [SerializeField] private int m_forceBuildingIndex;
#endif
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

        private void Update()
        {
            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R))
            {
                m_buttons[0].SetBuildingPrefab(ForceGetBuildingPrefabAt(m_forceType,m_forceBuildingIndex));
            }
            #endif
        }

        private GameObject ForceGetBuildingPrefabAt(int type, int index)
        {
            return type == 0 
                ? m_buildingContainer.DefensiveBuilding[index].Prefab 
                : m_buildingContainer.OffensiveBuilding[index].Prefab;
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


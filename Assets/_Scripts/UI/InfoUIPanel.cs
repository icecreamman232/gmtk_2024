using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class InfoUIPanel : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private OnClickBuildingEvent m_onClickBuildingEvent;
        [Header("Panels")]
        [SerializeField] private BuildingInfoUIPanel m_buildingInfoUIPanel;
        private void OnEnable()
        {
            m_onClickBuildingEvent.AddListener(OnShowBuildingInfo);
        }

        private void OnDisable()
        {
            m_onClickBuildingEvent.RemoveListener(OnShowBuildingInfo);
        }

        private void OnShowBuildingInfo(OnClickBuildingEventData data)
        {
            if (data.BuildingData.DefenseData != null)
            {
                m_buildingInfoUIPanel.DisplayInfo(data.BuildingData.BuildingName,data.BuildingData.DefenseData);
            }
            else if (data.BuildingData.OffenseData != null)
            {
                m_buildingInfoUIPanel.DisplayInfo(data.BuildingData.BuildingName,data.BuildingData.OffenseData);
            }
        }
    }
}


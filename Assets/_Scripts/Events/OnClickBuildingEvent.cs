using System;
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/OnClick Building Event")]
    public class OnClickBuildingEvent : ScriptableObject
    {
        protected Action<OnClickBuildingEventData> m_listeners;
    
        public void AddListener(Action<OnClickBuildingEventData> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<OnClickBuildingEventData> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(OnClickBuildingEventData data)
        {
            m_listeners?.Invoke(data);
        }
    }

    public struct OnClickBuildingEventData
    {
        public BuildingData BuildingData;
    }
}

using System;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.UI;
using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public enum BuildingState
    {
        IDLE,
        READY_TO_BUILD,
        BUILDING,
        DAMAGED,
    }
    
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private BuildingState m_curState;
        [SerializeField] private bool m_isPermit;
        [SerializeField] private SpriteRenderer m_bodyRenderer;
        [SerializeField] private BuildingData m_data;
        [SerializeField] private BuildingTimeBarUI m_timeBar;
        [SerializeField] private BuildingHealth m_health;
        [SerializeField] private OnClickBuildingEvent m_onClickBuildingEvent;
        [SerializeField] private SelectionFrameController m_selectionFrameController;
        
        private float m_curBuildTime;

        public Action<BuildingState> OnStateChange;
        
        public Sprite Icon => m_data.Icon;

        public BuildingState CurrentState => m_curState;

        public int Price => m_data.Price;
        public Color IconColor => m_data.AvailableColor;
        
        public bool IsPermit
        {
            set => m_isPermit = value;
            get => m_isPermit;
        }

        private void Start()
        {
            if (m_data.DefenseData != null)
            {
                m_health.Initialize(m_data.DefenseData.MaxHP);
            }
            else
            {
                m_health.Initialize(m_data.OffenseData.MaxHP);
            }
        }

        private void Update()
        {
            switch (m_curState)
            {
                case BuildingState.IDLE:
                    break;
                case BuildingState.BUILDING:
                    m_curBuildTime += Time.deltaTime;
                    m_timeBar.UpdateFillBar(MathHelpers.Remap(m_curBuildTime,0,m_data.BuildTime,0,1));
                    if (m_curBuildTime >= m_data.BuildTime)
                    {
                        m_bodyRenderer.color = m_data.AvailableColor;
                        SetBuildingState(BuildingState.IDLE);
                    }
                    break;
                case BuildingState.DAMAGED:
                    break;
            }
        }

        public void Deselect()
        {
            m_selectionFrameController.HideSelection();
        }
        
        public void OnBeingClickedOn()
        {
            var newEvent = new OnClickBuildingEventData
            {
                BuildingData = m_data
            };
            
            m_selectionFrameController.ShowSelection();
            
            m_onClickBuildingEvent.Raise(newEvent);
        }
        
        public void SetBuildingState(BuildingState newState)
        {
            m_curState = newState;
            OnStateChange?.Invoke(newState);
        }
    }
}


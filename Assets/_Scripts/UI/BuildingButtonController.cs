using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class BuildingButtonController : Button
    {
        [SerializeField] private bool m_isClicked;
        [SerializeField] private GameObject m_buildingPrefab;
        [SerializeField] private ActionEvent m_onLeftMouseClick;
        [SerializeField] private GameObjectEvent m_assignToCursorEvent;

        private BuildingButtonPanel m_panelRef;
        protected override void OnEnable()
        {
            m_onLeftMouseClick.AddListener(OnLeftMouseClickInWorld);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            m_onLeftMouseClick.RemoveListener(OnLeftMouseClickInWorld);
            base.OnDisable();
        }

        public void InitBuildingButton(BuildingButtonPanel panelRef)
        {
            m_panelRef = panelRef;
        }

        public void SetBuildingPrefab(GameObject newBuildingPrefab)
        {
            m_buildingPrefab = newBuildingPrefab;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (m_isClicked) return;
            m_isClicked = true;
            AssignBuildingPrefabToCursor();
        }

        private void OnLeftMouseClickInWorld()
        {
            m_isClicked = false;
        }
        
        private void AssignBuildingPrefabToCursor()
        {
            if (m_buildingPrefab == null)
            {
                Debug.LogError("Building Prefab is null!");
                return;
            }
            m_assignToCursorEvent.Raise(m_buildingPrefab);
        }
    }
}


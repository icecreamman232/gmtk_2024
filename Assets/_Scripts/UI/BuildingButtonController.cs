using JustGame.Scripts.Defense;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class BuildingButtonController : Button
    {
        [SerializeField] private Image m_icon;
        [SerializeField] private bool m_isClicked;
        [SerializeField] private GameObject m_buildingPrefab;
        [SerializeField] private BoolEvent m_onLeftMouseClick;
        [SerializeField] private GameObjectEvent m_assignToCursorEvent;

        private BuildingButtonPanel m_panelRef;
        private int m_price;
        
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
            var buildingController = newBuildingPrefab.GetComponent<BuildingController>();
            if (buildingController == null)
            {
                Debug.LogError("Missing building icon");
                return;
            }

            m_price = buildingController.Price;

            m_icon.sprite = buildingController.Icon;
            m_icon.color = buildingController.IconColor;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (m_isClicked) return;
            
            //Check if player can buy this building
            if (!ResourceManager.Instance.CanBuy(m_price)) return;
            
            m_isClicked = true;
            AssignBuildingPrefabToCursor();
        }

        private void OnLeftMouseClickInWorld(bool value)
        {
            if (!m_isClicked) return;
            m_isClicked = false;
            if (!value)
            {
                return;
            }
            SetBuildingPrefab(m_panelRef.GetRandomBuildingPrefab());
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


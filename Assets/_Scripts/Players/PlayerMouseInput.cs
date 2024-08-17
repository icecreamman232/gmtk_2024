using JustGame.Scripts.Defense;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerMouseInput : MonoBehaviour
    {
        [SerializeField] private Camera m_mainCamera;
        [SerializeField] private ActionEvent m_onLeftMouseClickInWorld;
        [SerializeField] private GameObjectEvent m_buildingBtnToCursorEvent;
        [SerializeField] private Vector3Event m_placeBuildingPosEvent;

        private GameObject m_assignedBuilding;

        private void OnEnable()
        {
            m_buildingBtnToCursorEvent.AddListener(OnAssignBuildingToCursor);
        }
        private void OnDisable()
        {
            m_buildingBtnToCursorEvent.RemoveListener(OnAssignBuildingToCursor);
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_onLeftMouseClickInWorld.Raise();
                if (m_assignedBuilding != null)
                {
                    PlaceBuildingIntoWorld();
                }
                else
                {
                    if (IsClickOnBuildingInWorld(out var result))
                    {
                        var controller = result.transform.parent.GetComponent<BuildingController>();
                        if (controller != null)
                        {
                            controller.OnBeingClickedOn();
                        }
                    }
                }
            }

            if (m_assignedBuilding != null)
            {
                UpdateAssignBuilding();
            }
        }

        private bool IsClickOnBuildingInWorld(out Collider2D resultCollider)
        {
            resultCollider = null;
            var result =Physics2D.Raycast(GetWorldMousePos(), Vector2.down, 10,LayerManager.BuildingLayerMask);
            if (result.collider != null)
            {
                resultCollider = result.collider;
                return true;
            }

            return false;
        }
        
        private Vector3 GetWorldMousePos()
        {
            var pos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
        
        private void UpdateAssignBuilding()
        {
            m_assignedBuilding.transform.position = GetWorldMousePos();
        }
        
        private void OnAssignBuildingToCursor(GameObject prefab)
        {
            //Can only assign 1 building at a time
            if (m_assignedBuilding != null) return;
    
            m_assignedBuilding = Instantiate(prefab, transform);
        }

        private void PlaceBuildingIntoWorld()
        {
            //Snap building to grid
            var curPos = m_assignedBuilding.transform.position;
            curPos.x = Mathf.Round(curPos.x);
            curPos.y = Mathf.Round(curPos.y);
            m_assignedBuilding.transform.position = curPos;
            m_placeBuildingPosEvent.Raise(curPos);
            
            //Set state to "Building" which require player to go nearby and build it
            var controller = m_assignedBuilding.GetComponent<BuildingController>();
            controller.SetBuildingState(BuildingState.READY_TO_BUILD);
            
            ResourceManager.Instance.Spent(controller.Price);
            
            m_assignedBuilding.transform.parent = null;
            m_assignedBuilding = null;
        }
    }
}


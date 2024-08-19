using JustGame.Scripts.World;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class UpgradeTreeButton : Button
    {
        [SerializeField] private CanvasGroup m_popupCanvasGroup;
        //TODO:Refactor to use event here
        [SerializeField] private TreeOfLifeController m_controller;
        
        protected override void OnEnable()
        {
            HidePopup();
            base.OnEnable();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            TryUpgradeTree();
            base.OnPointerClick(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            ShowPopup();
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            HidePopup();
            base.OnPointerExit(eventData);
        }

        private void TryUpgradeTree()
        {
            m_controller.UpgradeTree();
        }

        private void ShowPopup()
        {
            m_popupCanvasGroup.alpha = 1;
        }

        private void HidePopup()
        {
            m_popupCanvasGroup.alpha = 0;
        }
    } 
}


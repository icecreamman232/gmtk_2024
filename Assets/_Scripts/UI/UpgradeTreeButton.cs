using JustGame.Scripts.World;
using TMPro;
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
        [SerializeField] private Image m_curIcon;
        [SerializeField] private Image m_nextIcon;
        [SerializeField] private TextMeshProUGUI m_priceTxt;

        protected override void Start()
        {
            HidePopup();
            base.Start();
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
            m_curIcon.sprite = m_controller.CurrentTreeIcon;
            m_nextIcon.sprite = m_controller.NextTreeIcon;
            m_priceTxt.text = m_controller.PriceText;
        }

        private void ShowPopup()
        {
            m_curIcon.sprite = m_controller.CurrentTreeIcon;
            m_nextIcon.sprite = m_controller.NextTreeIcon;
            m_priceTxt.text = m_controller.PriceText;
            m_popupCanvasGroup.alpha = 1;
        }

        private void HidePopup()
        {
            m_popupCanvasGroup.alpha = 0;
        }
    } 
}


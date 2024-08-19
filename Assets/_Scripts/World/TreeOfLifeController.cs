using System;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.World
{
    public class TreeOfLifeController : MonoBehaviour
    {
        [SerializeField] private int m_treeLevel;
        [SerializeField] private TreeOfLifeUpgradeData m_treeOfLifeUpgradeData;
        [SerializeField] private SpriteRenderer m_spriteRenderer;

        public string PriceText => m_treeLevel < 5 
            ? m_treeOfLifeUpgradeData.UpgradePrice[m_treeLevel - 1].ToString() 
            : m_treeOfLifeUpgradeData.UpgradePrice[4].ToString();
        public Sprite CurrentTreeIcon => m_treeOfLifeUpgradeData.TreeSprites[m_treeLevel - 1];
        public Sprite NextTreeIcon => m_treeLevel < 5 ? m_treeOfLifeUpgradeData.TreeSprites[m_treeLevel] : null;
        
        private void Start()
        {
            m_treeLevel = 1;
            m_spriteRenderer.sprite = m_treeOfLifeUpgradeData.TreeSprites[m_treeLevel - 1];
        }

        public void UpgradeTree()
        {
            if (m_treeLevel >= 5)
            {
                return;
            }

            if(!ResourceManager.Instance.CanBuy(m_treeOfLifeUpgradeData.UpgradePrice[m_treeLevel - 1]))
            {
                return;
            }
            ResourceManager.Instance.Spent(m_treeOfLifeUpgradeData.UpgradePrice[m_treeLevel - 1]);
            m_treeLevel++;
            m_spriteRenderer.sprite = m_treeOfLifeUpgradeData.TreeSprites[m_treeLevel - 1];
        }
    }
}


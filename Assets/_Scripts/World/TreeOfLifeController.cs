using System;
using UnityEngine;

namespace JustGame.Scripts.World
{
    public class TreeOfLifeController : MonoBehaviour
    {
        [SerializeField] private int m_treeLevel;
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private Sprite[] m_treeSprites;

        private void Start()
        {
            m_treeLevel = 1;
            m_spriteRenderer.sprite = m_treeSprites[m_treeLevel - 1];
        }

        private void UpgradeTree()
        {
            m_treeLevel++;
            m_spriteRenderer.sprite = m_treeSprites[m_treeLevel - 1];
        }
    }
}


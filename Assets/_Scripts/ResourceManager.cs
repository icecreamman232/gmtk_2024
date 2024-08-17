using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [Header("Events")] 
        [SerializeField] private IntEvent m_updateGoldUIEvent;
        [Header("Currency")]
        [SerializeField] private int m_curGold;
        
        private void Start()
        {
            m_updateGoldUIEvent.Raise(m_curGold);
        }

        public bool CanBuy(int price)
        {
            return m_curGold >= price;
        }
        
        public void Spent(int goldSpent)
        {
            m_curGold -= goldSpent;
            if (m_curGold <= 0)
            {
                m_curGold = 0;
            }
            m_updateGoldUIEvent.Raise(m_curGold);
        }

        public void Earn(int goldEarn)
        {
            m_curGold += goldEarn;
            m_updateGoldUIEvent.Raise(m_curGold);
        }
    }
}

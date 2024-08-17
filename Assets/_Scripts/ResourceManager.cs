using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [SerializeField] private int m_curGold;

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
        }

        public void Earn(int goldEarn)
        {
            m_curGold += goldEarn;
        }
    }
}

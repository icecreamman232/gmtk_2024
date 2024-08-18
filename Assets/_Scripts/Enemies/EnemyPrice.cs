using UnityEngine;
using ResourceManager = JustGame.Scripts.Managers.ResourceManager;

namespace JustGame.Scripts.Enemy
{
    public class EnemyPrice : MonoBehaviour
    {
        [SerializeField] private int m_gold;
        private EnemyHealth m_health;

        private void Start()
        {
            m_health = GetComponent<EnemyHealth>();
            m_health.OnDeath += OnEarnGold;
        }

        private void OnDestroy()
        {
            m_health.OnDeath -= OnEarnGold;
        }

        private void OnEarnGold()
        {
            ResourceManager.Instance.Earn(m_gold);
        }
    }
}


using JustGame.Scripts.Data;
using JustGame.Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.World
{
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private PointController m_startingPoint;
        [SerializeField] private MonsterContainer m_monsterContainer;

        [Header("Placeholder Data")] 
        [SerializeField] private int m_monsterLevel;
        [SerializeField] private float m_minDelayTime;
        [SerializeField] private float m_delayBetween2Spawn;
        [SerializeField] private float m_reduceDelayTimeRate;
        [SerializeField] private float m_timeToUpgrade;

        private float m_delayTimer;
        private float m_upgradeTimer;

        private void Update()
        {
            m_delayTimer += Time.deltaTime;
            m_upgradeTimer += Time.deltaTime;
            if (m_delayTimer >= m_delayBetween2Spawn)
            {
                m_delayTimer = 0;
                SpawnMonster();
            }

            if (m_upgradeTimer >= m_timeToUpgrade)
            {
                m_upgradeTimer = 0;
                UpgradeMonsterWave();
            }
        }

        private void UpgradeMonsterWave()
        {
            if (m_delayBetween2Spawn >= m_minDelayTime)
            {
                m_delayBetween2Spawn -= m_reduceDelayTimeRate;
            }

            m_monsterLevel++;
        }
        
        private void SpawnMonster()
        {
            var randomIndex = Random.Range(0, m_monsterContainer.Monsters.Length);
            var monster = Instantiate(m_monsterContainer.Monsters[randomIndex], m_startingPoint.transform.position,
                Quaternion.identity);
            var enemyController = monster.GetComponent<EnemyController>();
            enemyController.Initialize(m_startingPoint, m_monsterLevel);
        }
    }
}


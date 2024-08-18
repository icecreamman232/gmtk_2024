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
        [SerializeField] private float m_delayBetween2Spawn;

        private float m_delayTimer;

        private void Update()
        {
            m_delayTimer += Time.deltaTime;
            if (m_delayTimer >= m_delayBetween2Spawn)
            {
                m_delayTimer = 0;
                SpawnMonster();
            }
        }

        private void SpawnMonster()
        {
            var randomIndex = Random.Range(0, m_monsterContainer.Monsters.Length);
            var monster = Instantiate(m_monsterContainer.Monsters[randomIndex], m_startingPoint.transform.position,
                Quaternion.identity);
            var enemyController = monster.GetComponent<EnemyController>();
            enemyController.Initialize(m_startingPoint);
        }
    }
}


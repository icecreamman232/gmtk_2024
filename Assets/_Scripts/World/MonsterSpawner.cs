using System;
using System.Collections.Generic;
using JustGame.Scripts.Data;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.World
{
    public class MonsterSpawner : MonoBehaviour
    {
        [Header("Monster Numbers")]
        [SerializeField] private int m_spawnedNumber;
        [SerializeField] private int m_aliveNumber;
        [SerializeField] private int m_aliveEliteNumber;
        [SerializeField] private int m_deadNumber;
        [SerializeField] private int m_deadEliteNumber;
        
        [SerializeField] private PointController m_startingPoint;
        [SerializeField] private MonsterContainer m_monsterContainer;
        [SerializeField] private EnemyDeathEvent m_enemyDeathEvent;
        
        [Header("Placeholder Data")] 
        [SerializeField] private int m_monsterLevel;
        [SerializeField] private float m_minDelayTime;
        [SerializeField] private float m_delayBetween2Spawn;
        [SerializeField] private float m_reduceDelayTimeRate;
        [SerializeField] private float m_timeToUpgrade;
        [Header("Merge")] 
        [SerializeField] private int m_numberToMerge;

        [SerializeField] private GameObject m_bigSlimePrefab;

        private float m_delayTimer;
        private float m_upgradeTimer;
        private HashSet<EnemyHealth> m_aliveMonsterList;
        private HashSet<EnemyHealth> m_aliveEliteMonsterList;

        private void Start()
        {
            m_aliveMonsterList = new HashSet<EnemyHealth>();
            m_aliveEliteMonsterList = new HashSet<EnemyHealth>();
        }

        private void OnEnable()
        {
            m_enemyDeathEvent.AddListener(OnEnemyDeath);
        }

        private void OnEnemyDeath(EnemyHealth enemyHealth)
        {
            if (enemyHealth.IsElite)
            {
                m_aliveEliteMonsterList.Remove(enemyHealth);
                m_aliveEliteNumber--;
                m_deadEliteNumber++;
            }
            else
            {
                m_deadNumber++;
                m_aliveNumber--;
                m_aliveMonsterList.Remove(enemyHealth);
            }
        }

        private void OnDisable()
        {
            m_enemyDeathEvent.RemoveListener(OnEnemyDeath);
        }

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

            if (m_aliveNumber >= m_numberToMerge)
            {
                MergeMonster();
            }
        }

        private void MergeMonster()
        {
            Debug.Log($"<color=orange>Monsters has been merged into bigger</color>");
            var count = m_numberToMerge;
            var listToKill = new List<EnemyHealth>();
            foreach (var alive in m_aliveMonsterList)
            {
                listToKill.Add(alive);
                count--;
                if (count <= 0)
                {
                    break;
                }
            }

            foreach (var toKill in listToKill)
            {
                toKill.ManualKill();
            }

            SpawnSpecificMonster(m_bigSlimePrefab);
        }

        private void UpgradeMonsterWave()
        {
            if (m_delayBetween2Spawn >= m_minDelayTime)
            {
                m_delayBetween2Spawn -= m_reduceDelayTimeRate;
            }
            m_monsterLevel++;
            Debug.Log($"<color=orange>Upgrade monster to lvl {m_monsterLevel}</color>");
        }

        private void SpawnSpecificMonster(GameObject prefab)
        {
            var monster = Instantiate(prefab, m_startingPoint.transform.position,
                Quaternion.identity);
            var enemyController = monster.GetComponent<EnemyController>();
            enemyController.Initialize(m_startingPoint, m_monsterLevel);

            m_aliveEliteMonsterList.Add(monster.GetComponent<EnemyHealth>());
            
            m_spawnedNumber++;
            m_aliveEliteNumber++;
        }
        
        private void SpawnMonster()
        {
            var randomIndex = Random.Range(0, m_monsterContainer.Monsters.Length);
            var monster = Instantiate(m_monsterContainer.Monsters[randomIndex], m_startingPoint.transform.position,
                Quaternion.identity);
            var enemyController = monster.GetComponent<EnemyController>();
            enemyController.Initialize(m_startingPoint, m_monsterLevel);

            m_aliveMonsterList.Add(monster.GetComponent<EnemyHealth>());
            
            m_spawnedNumber++;
            m_aliveNumber++;
        }
    }
}


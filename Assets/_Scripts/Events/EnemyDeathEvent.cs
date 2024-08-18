using System;
using JustGame.Scripts.Enemy;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Enemy Death Event")]
    public class EnemyDeathEvent : ScriptableObject
    {
        protected Action<EnemyHealth> m_listeners;
        
        public void AddListener(Action<EnemyHealth> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<EnemyHealth> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(EnemyHealth enemyHealth)
        {
            m_listeners?.Invoke(enemyHealth);
        }
    } 
}

using System;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/GameObject Event")]
    public class GameObjectEvent : ScriptableObject
    {
        protected Action<GameObject> m_listeners;
    
        public void AddListener(Action<GameObject> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<GameObject> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(GameObject obj)
        {
            m_listeners?.Invoke(obj);
        }
    } 
}


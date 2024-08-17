using System;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Damage
{
    public class FindTargetInArea : MonoBehaviour
    {
        [SerializeField] private LayerMask m_targetMask;
        [SerializeField] private bool m_isActive;
        public Action<Collider2D> OnFoundTarget;

        public void Activate()
        {
            m_isActive = true;
        }

        public void Deactivate()
        {
            m_isActive = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!m_isActive) return;
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask))
            {
                OnFoundTarget.Invoke(other);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!m_isActive) return;
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask))
            {
                OnFoundTarget.Invoke(other);
            }
        }
    }
}


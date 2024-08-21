using System;
using JustGame.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnCollide : MonoBehaviour
{
    [SerializeField] private bool m_triggerOnEnter;
    [SerializeField] private bool m_triggerOnStay;
    [SerializeField] private bool m_triggerOnExit;
    
    [SerializeField] private LayerMask m_triggerLayerMask;
    [SerializeField] private string m_triggerTag;

    public UnityEvent<Collider2D> UnityEventTrigger;
    public Action<Collider2D> OnTriggerHit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_triggerOnEnter) return;
        if (!string.IsNullOrEmpty(m_triggerTag) && !other.gameObject.CompareTag(m_triggerTag))
        {
            return;
        }
        
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_triggerLayerMask))
        {
            UnityEventTrigger.Invoke(other);
            OnTriggerHit?.Invoke(other);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!m_triggerOnStay) return;
        if (!string.IsNullOrEmpty(m_triggerTag) && !other.gameObject.CompareTag(m_triggerTag))
        {
            return;
        }
        
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_triggerLayerMask))
        {
            UnityEventTrigger.Invoke(other);
            OnTriggerHit?.Invoke(other);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!m_triggerOnExit) return;
        if (!string.IsNullOrEmpty(m_triggerTag) && !other.gameObject.CompareTag(m_triggerTag))
        {
            return;
        }
        
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_triggerLayerMask))
        {
            UnityEventTrigger.Invoke(other);
            OnTriggerHit?.Invoke(other);
        }
    }
}

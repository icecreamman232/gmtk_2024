using System;
using JustGame.Scripts.Defense;
using JustGame.Scripts.Managers;
using UnityEngine;

public class EarnMoneyOverTimeController : MonoBehaviour
{
    [SerializeField] private float m_frequent;
    [SerializeField] private int m_earning;
    [SerializeField] private BuildingController m_buildingController;
    [SerializeField] private Animator m_animator;

    private int m_trigger_earning_anim = Animator.StringToHash("Trigger_Earning");
    private float m_timer;

    private void Update()
    {
        if (m_buildingController.CurrentState != BuildingState.IDLE) return;
        
        m_timer += Time.deltaTime;
        if (m_timer >= m_frequent)
        {
            m_timer = 0;
            ResourceManager.Instance.Earn(m_earning);
            m_animator.SetTrigger(m_trigger_earning_anim);
        }
    }
}

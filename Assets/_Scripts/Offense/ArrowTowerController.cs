using System;
using JustGame.Scripts.Damage;
using JustGame.Scripts.Defense;
using UnityEngine;

public class ArrowTowerController : MonoBehaviour
{
    [SerializeField] private BuildingController m_buildingController;
    [SerializeField] private ArrowShooter m_shooter;
    [SerializeField] private FindTargetInArea m_targetFinder;

    private void Start()
    {
        m_targetFinder.Deactivate();
    }

    private void OnEnable()
    {
        m_targetFinder.OnFoundTarget += OnFoundTarget;
        m_buildingController.OnStateChange += OnBuildingStateChange;
    }

    private void OnBuildingStateChange(BuildingState newState)
    {
        if (newState == BuildingState.IDLE)
        {
            m_targetFinder.Activate();
        }
    }

    private void OnDisable()
    {
        m_targetFinder.OnFoundTarget -= OnFoundTarget;
        m_buildingController.OnStateChange -= OnBuildingStateChange;
    }

    private void OnFoundTarget(Collider2D target)
    {
        m_shooter.Shoot(target.transform.position);
    }
}

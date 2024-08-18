using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class SlimeEnemyController : EnemyController
    {
        [SerializeField] private EnemyAttack m_enemyAttack;
        protected override void OnHitObstacle(Collider2D obstacle)
        {
            if (LayerManager.IsInLayerMask(obstacle.gameObject.layer, LayerManager.BuildingLayerMask))
            {
                m_enemyAttack.StartAttack();
            }
            base.OnHitObstacle(obstacle);
        }
    }
}


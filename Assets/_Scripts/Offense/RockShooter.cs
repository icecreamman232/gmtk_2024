using UnityEngine;

namespace JustGame.Scripts.Damage
{
    public class RockShooter : Shooter
    {
        internal enum ShootDirection
        {
            Up,
            Down,
            Left,
            Right
        };

        [SerializeField] private ShootDirection m_shootDirection;
        [SerializeField] private float m_maxRange;

        public override void Shoot(Vector2 position)
        {
            if (m_isDelay) return;
            
            var projectileGO = m_projectilePooler.GetPooledGameObject();
            var projectile = projectileGO.GetComponent<Projectile>();
            projectile.transform.position = m_shootPivot.position;
            projectile.transform.rotation = Quaternion.identity;

            Vector2 targetPos = m_shootPivot.position;
            
            switch (m_shootDirection)
            {
                case ShootDirection.Up:
                    targetPos += (Vector2.up * m_maxRange);
                    break;
                case ShootDirection.Down:
                    targetPos += (Vector2.down * m_maxRange);
                    break;
                case ShootDirection.Left:
                    targetPos += (Vector2.left * m_maxRange);
                    break;
                case ShootDirection.Right:
                    targetPos += (Vector2.right * m_maxRange);
                    break;
            }
            
            if (projectile != null)
            {
                projectile.SetMoveTo(targetPos);
                StartCoroutine(OnDelayShoot());
            }
        }
    }
}


using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public interface Damageable
    {
        public void TakeDamage(float damage, float invulnerableDuration, GameObject instigator);
    }
}


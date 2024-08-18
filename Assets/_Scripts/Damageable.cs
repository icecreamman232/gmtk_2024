
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.Defense
{
    public interface Damageable
    {
        public void TakeDamage(AttackType atkType, float damage, float invulnerableDuration, GameObject instigator);
    }
}


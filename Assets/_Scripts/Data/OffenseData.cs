using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Offense Data")]
    public class OffenseData : ScriptableObject
    {
        public AttackType AttackType;
        public ArmorType ArmorType;
        public float MaxHP;
    }

    public enum AttackType
    {
        Normal,
        Piercing,
        Magic,
    }
}
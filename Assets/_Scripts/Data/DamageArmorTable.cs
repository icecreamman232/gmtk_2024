using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Damage Armor Table")]
    public class DamageArmorTable : ScriptableObject
    {
        [Header("Normal Atk Type")] 
        public float NormalToLight;
        public float NormalToNormal;
        public float NormalToMassive;
        [Header("Piercing Atk Type")]
        public float PiercingToLight;
        public float PiercingToNormal;
        public float PiercingToMassive;
        [Header("Magic Atk Type")]
        public float MagicToLight;
        public float MagicToNormal;
        public float MagicToMassive;

        public float GetFinalDamage(float damage, AttackType atkType, ArmorType armorType)
        {
            switch (atkType)
            {
                case AttackType.Normal:
                    switch (armorType)
                    {
                        case ArmorType.Light:
                            return damage * NormalToLight/100;
                        case ArmorType.Normal:
                            return damage * NormalToNormal/100;
                        case ArmorType.Massive:
                            return damage * NormalToMassive/100;
                    }
                    break;
                case AttackType.Piercing:
                    switch (armorType)
                    {
                        case ArmorType.Light:
                            return damage * PiercingToLight/100;
                        case ArmorType.Normal:
                            return damage * PiercingToNormal/100;
                        case ArmorType.Massive:
                            return damage * PiercingToMassive/100;
                    }
                    break;
                case AttackType.Magic:
                    switch (armorType)
                    {
                        case ArmorType.Light:
                            return damage * MagicToLight/100;
                        case ArmorType.Normal:
                            return damage * MagicToNormal/100;
                        case ArmorType.Massive:
                            return damage * MagicToMassive/100;
                    }
                    break;
            }

            return damage;
        }
        
    }
}


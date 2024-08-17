using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Defense Data")]
    public class DefenseData : ScriptableObject
    {
        public ArmorType ArmorType;
        public float MaxHP;
    }

    public enum ArmorType
    {
        Light,
        Normal,
        Massive,
    }
}


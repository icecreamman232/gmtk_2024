using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Building Data",fileName = "BuildingData")]
    public class BuildingData : ScriptableObject
    {
        public string ID;
        public Color AvailableColor;
        public GameObject Prefab;
        public Sprite Icon;
        public float BuildTime;
        public int Price;

        public DefenseData DefenseData;
    }
}


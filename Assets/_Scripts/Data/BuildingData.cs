using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Building Data",fileName = "BuildingData")]
    public class BuildingData : ScriptableObject
    {
        public string ID;
        public string BuildingName;
        public GameObject Prefab;
        public Sprite Icon;
        public Sprite WhiteIcon;
        public float BuildTime;
        public int Price;

        public OffenseData OffenseData;
        public DefenseData DefenseData;
    }
}


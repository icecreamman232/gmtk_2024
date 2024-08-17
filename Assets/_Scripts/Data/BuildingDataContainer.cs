using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Building Container",fileName="Building Data Container")]
    public class BuildingDataContainer : ScriptableObject
    {
        public GameObject[] DefensiveBuilding;

        public GameObject[] OffensiveBuilding;
    }
}

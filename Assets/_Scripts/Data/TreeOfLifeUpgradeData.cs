using UnityEngine;

namespace  JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Tree of Life")]
    public class TreeOfLifeUpgradeData : ScriptableObject
    {
        public int[] UpgradePrice;
        public Sprite[] TreeSprites;
    }
}

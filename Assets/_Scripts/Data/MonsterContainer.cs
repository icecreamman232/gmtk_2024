using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Monster Container")]
    public class MonsterContainer : ScriptableObject
    {
        public GameObject[] Monsters;
    }
}


using System.Collections.Generic;

using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "SpawnData", order = 0)]
    public class SpawnData : ScriptableObject
    {
        [SerializeField] private GameObject[] _prefabs;

        public IList<GameObject> Prefabs => _prefabs;
    }
}
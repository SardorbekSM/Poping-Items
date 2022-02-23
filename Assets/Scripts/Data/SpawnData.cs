using System.Collections.Generic;

using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "SpawnData", order = 0)]
    public class SpawnData : ScriptableObject
    {
        [SerializeField] private GameObject[] _prefabs;

        public IEnumerable<GameObject> Prefabs => _prefabs;
    }
}
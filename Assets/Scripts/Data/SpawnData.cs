
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class SpawnData : ScriptableObject
    {
        [SerializeField] private float _spawnDuration = 1f;

        [SerializeField] private GameObject[] _prefabs;
        
        public float SpawnDuration => _spawnDuration;
        
        public GameObject[] Prefabs => _prefabs;
    }
}

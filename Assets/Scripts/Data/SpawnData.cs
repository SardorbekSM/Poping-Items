
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class SpawnData : ScriptableObject
    {
        [SerializeField, Min(1)] private int _spawnCount = 1;
        [SerializeField] private float _spawnDuration = 1f;
        [SerializeField] private float _lifeTime = 10f;

        [SerializeField] private GameObject[] _prefabs;
        
        public int SpawnCount => _spawnCount;
        public float SpawnDuration => _spawnDuration;
        public float LifeTime => _lifeTime;
        
        public GameObject[] Prefabs => _prefabs;
    }
}

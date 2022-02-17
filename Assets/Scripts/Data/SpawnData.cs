
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class SpawnData : ScriptableObject
    {
        [SerializeField, Min(0.01f)] private float _spawnDuration = 1f;
        [SerializeField, Min(0.01f)] private float _itemsLifeTime;
        [SerializeField] private GameObject[] _prefabs;


        public float SpawnDuration => _spawnDuration;
        public float ItemsLifeTime => _itemsLifeTime;

        public GameObject[] Prefabs => _prefabs;

    }
}

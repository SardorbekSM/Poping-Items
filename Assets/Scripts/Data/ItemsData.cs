using System.Collections.Generic;

using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class ItemsData : ScriptableObject
    {
        [SerializeField, Min(0.01f)] private float _spawnDuration = 1f;
        [SerializeField, Min(0.01f)] private float _itemsLifeTime;
        [SerializeField] private SpawnData _prefabs;

        public float SpawnDuration => _spawnDuration;
        public float ItemsLifeTime => _itemsLifeTime; // Не использется как то надо использовать
        public IEnumerable<GameObject> Prefabs => _prefabs.Prefabs;

    }
}

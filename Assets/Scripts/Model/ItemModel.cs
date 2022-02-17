using Data;
using UnityEngine;

namespace Model
{
    public class ItemModel
    {
        public float SpawnDuration => _spawnData.SpawnDuration;
        public float ItemLifeTime => _spawnData.ItemsLifeTime;

        public GameObject[] Prefabs => _spawnData.Prefabs;

        private readonly SpawnData _spawnData;

        public ItemModel(SpawnData spawnData)
        {
            _spawnData = spawnData;
        }
    }
}
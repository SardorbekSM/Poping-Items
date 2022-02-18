using System;
using Core.Pool;
using Core.Randomizer;
using Core.Spawner.Interfaces;
using Data;
using UnityEngine;

namespace Core.Spawner
{
    public sealed class SpawnerWithPool : ISpawnerBehaviour
    {
        public event Action<GameObject> OnInstantiatedObject = delegate {  };

        private readonly IPooler<GameObject> _pooler;
        private readonly ISpawner<GameObject> _spawner;

        private readonly ISpawnerContainer<GameObject> _spawnerContainer = new SpawnerContainer<GameObject>();

        public SpawnerWithPool(IRandomizer randomizer, ItemsData itemsData) // Как то надо убрать прямую связ с данными
        {
            _pooler = new RandomizerPooler(itemsData.Prefabs, randomizer);
            _spawner = new Spawner<GameObject>(_pooler);
        }

        public async void Spawn()
        {
            await _spawner.BeginSpawning(OnSpawnedObject);
        }

        public void OnSpawnedObject(GameObject spawnedObject)
        {
            spawnedObject.GetComponent<ISpawnableObject<IPooler<GameObject>>>()?.SetValue(_pooler);

            _spawnerContainer.SetValue(spawnedObject);

            OnInstantiatedObject.Invoke(spawnedObject);
        }

        public void Dispose()
        {
            _spawnerContainer?.Dispose();
            _pooler?.Dispose();
        }
    }
}

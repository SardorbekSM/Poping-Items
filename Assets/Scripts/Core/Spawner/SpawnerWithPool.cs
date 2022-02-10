﻿using System;
using Data;
using UnityEngine;
using VContainer;

namespace Core.Spawner
{
    public sealed class SpawnerWithPool : ISpawnerBehaviour<GameObject>
    {
        public event Action<GameObject> OnInstantiatedObject = delegate {  };

        private readonly IPooler<GameObject> _pooler;
        private readonly ISpawner<GameObject> _spawner;

        private readonly ISpawnerContainer<GameObject> _spawnerContainer = new SpawnerContainer<GameObject>();

        [Inject]
        public SpawnerWithPool(IRandomizer randomizer, SpawnData spawnData)
        {
            _pooler = new RandomizerPooler(spawnData.Prefabs, randomizer);
            _spawner = new Spawner<GameObject>(_pooler);
        }

        public async void Spawn(int count)
        {
            for (var i = 0; i < count; i++)
            {
                await _spawner.BeginSpawning(OnSpawnedObject);
            }
        }

        public void OnSpawnedObject(GameObject spawnedObject)
        {
            spawnedObject.GetComponent<ISpawnableObject<IPooler<GameObject>>>()?.SetValue(_pooler);

            _spawnerContainer.SetValue(spawnedObject);

            OnInstantiatedObject.Invoke(spawnedObject);
        }

        public void DestroySpawnedObject(GameObject spawnedGameObject)
        {
            _spawnerContainer.DestroySpawnedObject(spawnedGameObject);
            _pooler.Dispose();
        }

        public void Dispose()
        {
            _spawnerContainer.Dispose();
            _pooler?.Dispose();
        }
    }
}

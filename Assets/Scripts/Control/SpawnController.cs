using System;
using Core.Pool;
using Core.Randomizer;
using Core.Spawner;
using Core.Spawner.Interfaces;
using Data;
using UnityEngine;

namespace Control
{
    public sealed class SpawnController : ISpawnerBehaviour
    {
        private IPooler<GameObject> _pooler;
        private ISpawner<GameObject> _spawner;
        private readonly IRandomizer _randomizer = new UniqueValueRandomizer();
        private readonly ISpawnerContainer<GameObject> _spawnerContainer = new SpawnerContainer<GameObject>();

        public event Action<GameObject> OnInstantiatedObject = delegate {  };

        public void Initialize(SpawnData spawnData)
        {
            _pooler = new RandomizerPooler(spawnData.Prefabs, _randomizer);
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

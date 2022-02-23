using System;

using UnityEngine;

using Core.Pool;
using Core.Randomizer;
using Core.Spawner;
using Core.Spawner.Interfaces;

using Data;
using View;

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
            var itemView = spawnedObject.GetComponent<ItemView>();

            itemView.Reseted += () => _pooler.Return(spawnedObject);
            
            spawnedObject.gameObject.SetActive(true);
            
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

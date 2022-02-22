using System;
using Core.Pool;
using Core.Randomizer;
using Core.Spawner.Interfaces;
using Core.WaiterAsync;
using Data;
using UnityEngine;

namespace Core.Spawner
{
    public sealed class LoopedSpawner : ISpawnerBehaviour
    {
        private readonly IRandomizer _randomizer;
        
        private ILoopedAction _loopedActionAsync;
        private IPooler<GameObject> _pooler;
        private ISpawner<GameObject> _spawner;
        private ISpawnerContainer<GameObject> _spawnerContainer = new SpawnerContainer<GameObject>();

        public event Action<GameObject> OnInstantiatedObject = delegate {  };

        public LoopedSpawner(IRandomizer randomizer)
        {
            _loopedActionAsync = new LoopedActionAsync();
            _randomizer = randomizer;
        }

        public void Initialize(SpawnData spawnData, float duration)
        {
            _pooler = new RandomizerPooler(spawnData.Prefabs, _randomizer);
            _spawner = new Spawner<GameObject>(_pooler);
            _spawnerContainer = new SpawnerContainer<GameObject>();
            
            _loopedActionAsync.DoAction += Spawn;
            _loopedActionAsync.Begin(duration);
        }

        private async void Spawn()
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
            _loopedActionAsync.DoAction -= Spawn;
            _loopedActionAsync.EndLoop();
            _spawnerContainer?.Dispose();
            _pooler?.Dispose();
        }
    }
}

using System;
using Core.Spawner.Interfaces;
using UnityEngine;

namespace Core.Spawner
{
    public sealed class SpawnerWithPool : ISpawnerBehaviour
    {
        private readonly ISpawnerContainer<GameObject> _spawnerContainer = new SpawnerContainer<GameObject>();
        private ISpawner<GameObject> _spawner;

        public event Action<GameObject> OnInstantiatedObject = delegate {  };

        public void Initialize(IPooler<GameObject> pooler)
        {
            OnInstantiatedObject += _spawnerContainer.SetValue;
            _spawner = new Spawner<GameObject>(pooler);
        }

        public async void Spawn()
        {
            await _spawner.BeginSpawning(OnInstantiatedObject);
        }

        public void Dispose()
        {
            _spawnerContainer?.Dispose();
        }
    }
}

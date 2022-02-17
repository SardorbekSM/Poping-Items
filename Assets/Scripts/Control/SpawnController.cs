using System;
using Control.Interfaces;
using Core.Spawner;
using Core.Spawner.Interfaces;
using Core.WaiterAsync;
using Model;
using UnityEngine;
using VContainer.Unity;

namespace Control
{
    public class SpawnController
    {
        private readonly SpawnModel _spawnModel;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private LoopedActionAsync _loopedActionAsync;

        public event Action<GameObject> SpawnedObject;

        public SpawnController(ISpawnerBehaviour spawnerWithPool, SpawnModel spawnModel)
        {
            _spawnerWithPool = spawnerWithPool;
            _spawnModel = spawnModel;
        }

        public void StartControl()
        {
            _spawnerWithPool.Dispose();
            _spawnerWithPool.OnInstantiatedObject += SpawnedObject;
            _loopedActionAsync = new LoopedActionAsync();
            _loopedActionAsync.DoAction += _spawnerWithPool.Spawn;
            _loopedActionAsync.Begin(_spawnModel.SpawnDuration);
        }

        public void EndControl()
        {
            _spawnerWithPool.OnInstantiatedObject -= SpawnedObject;
            _loopedActionAsync.DoAction -= _spawnerWithPool.Spawn;
            _loopedActionAsync.EndLoop();
        }
    }
}

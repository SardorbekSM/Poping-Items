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
        private readonly ItemModel _itemModel;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private LoopedActionAsync _loopedActionAsync;

        public event Action<GameObject> SpawnedObject;

        public SpawnController(ISpawnerBehaviour spawnerWithPool, ItemModel itemModel)
        {
            _spawnerWithPool = spawnerWithPool;
            _itemModel = itemModel;
        }

        public void StartControl()
        {
            _spawnerWithPool.Dispose();
            _spawnerWithPool.OnInstantiatedObject += SpawnedObject;
            _loopedActionAsync = new LoopedActionAsync();
            _loopedActionAsync.DoAction += _spawnerWithPool.Spawn;
            _loopedActionAsync.Begin(_itemModel.SpawnDuration);
        }

        public void EndControl()
        {
            _spawnerWithPool.OnInstantiatedObject -= SpawnedObject;
            _loopedActionAsync.DoAction -= _spawnerWithPool.Spawn;
            _loopedActionAsync.EndLoop();
        }
    }
}

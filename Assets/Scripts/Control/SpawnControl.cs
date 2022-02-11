using Control.Interfaces;
using Core.Spawner;
using Core.WaiterAsync;
using Model;
using UnityEngine;
using VContainer.Unity;

namespace Control
{
    public class SpawnControl
    {
        private readonly SpawnModel _spawnModel;
        private readonly SpawnerWithPool _spawnerWithPool;
        private LoopedActionAsync _loopedActionAsync;

        public SpawnControl(SpawnerWithPool spawnerWithPool, SpawnModel spawnModel)
        {
            _spawnerWithPool = spawnerWithPool;
            _spawnModel = spawnModel;
        }

        public void StartControl()
        {
            _spawnerWithPool.Dispose();
            _loopedActionAsync = new LoopedActionAsync();
            _loopedActionAsync.DoAction += _spawnerWithPool.Spawn;
            EndControl();
            _loopedActionAsync.Begin(_spawnModel.SpawnDuration);
        }

        public void EndControl()
        {
            _loopedActionAsync.EndLoop();
        }
    }
}

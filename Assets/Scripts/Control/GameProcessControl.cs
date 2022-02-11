using Control.Interfaces;
using Core.Spawner;
using Core.WaiterAsync;
using Model;
using UnityEngine;
using VContainer.Unity;

namespace Control
{
    public class GameProcessControl
    {
        private readonly SpawnModel _spawnModel;
        private readonly SpawnerWithPool _spawnerWithPool;
        private LoopedActionAsync _loopedActionAsync;

        public GameProcessControl(SpawnerWithPool spawnerWithPool, SpawnModel spawnModel)
        {
            _spawnerWithPool = spawnerWithPool;
            _spawnModel = spawnModel;
        }

        public void BeginSpawn()
        {
            _spawnerWithPool.Dispose();
            _loopedActionAsync = new LoopedActionAsync();
            _loopedActionAsync.DoAction += _spawnerWithPool.Spawn;
            StopSpawn();
            _loopedActionAsync.Begin(_spawnModel.SpawnDuration);
        }

        public void StopSpawn()
        {
            _loopedActionAsync.EndLoop();
        }

        public void OnWin()
        {
            Debug.Log("You Win!");
        }
    }
}

using Core.Spawner;
using Core.WaiterAsync;
using Model;
using UnityEngine;
using VContainer;

namespace Control
{
    public class GameProcessControl : MonoBehaviour
    {
        private SpawnModel _spawnModel;
        private SpawnerWithPool _spawnerWithPool;
        private LoopedActionAsync _loopedActionAsync;

        [Inject]
        public void Inject(SpawnerWithPool spawnerWithPool, SpawnModel spawnModel)
        {
            _spawnerWithPool = spawnerWithPool;
            _spawnModel = spawnModel;
        }

        public void Start()
        {
            _spawnerWithPool.Dispose();
            _loopedActionAsync = new LoopedActionAsync();
            _loopedActionAsync.DoAction += BeginSpawn;
            StopLooping();
            _loopedActionAsync.Begin(_spawnModel.SpawnDuration);
        }

        private void BeginSpawn()
        {
            _spawnerWithPool.Spawn(_spawnModel.SpawnCount);
        }

        public void StopLooping()
        {
            _loopedActionAsync.EndLoop();
        }
    }
}

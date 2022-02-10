using Core.Spawner;
using Model;
using UnityEngine;
using VContainer;

namespace Control
{
    public class GameProcessControl : MonoBehaviour
    {
        private SpawnModel _spawnModel;
        private SpawnerWithPool _spawnerWithPool;
        private LoopedAction _loopedAction;

        [Inject]
        public void Inject(SpawnerWithPool spawnerWithPool, SpawnModel spawnModel)
        {
            _spawnerWithPool = spawnerWithPool;
            _spawnModel = spawnModel;
        }

        public void Start()
        {
            _loopedAction = new LoopedAction();
            _loopedAction.DoAction += BeginSpawn;
            StopLooping();
            _loopedAction.Begin(_spawnModel.SpawnDuration);
        }

        private void BeginSpawn()
        {
            _spawnerWithPool.Spawn(_spawnModel.SpawnCount);
        }

        public void StopLooping()
        {
            _loopedAction.EndLoop();
        }
    }
}

using Core.Spawner.Interfaces;
using Core.WaiterAsync;
using Data;
using UnityEngine;

namespace Model
{
    public class ItemModel
    {
        private readonly SpawnData _spawnData;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private LoopedActionAsync _loopedActionAsync; // Не инжектится на VContainer
        public float ItemLifeTime => _spawnData.ItemsLifeTime;
        public GameObject[] Prefabs => _spawnData.Prefabs;

        public ItemModel(SpawnData spawnData, ISpawnerBehaviour spawnerBehaviour)
        {
            _spawnData = spawnData;
            _spawnerWithPool = spawnerBehaviour;
        }

        public void BeginSpawn()
        {
            _loopedActionAsync = new LoopedActionAsync();
            _loopedActionAsync.DoAction += _spawnerWithPool.Spawn;
            _loopedActionAsync.Begin(_spawnData.SpawnDuration);
        }
        
        public void EndSpawn()
        {
            _loopedActionAsync.DoAction -= _spawnerWithPool.Spawn;
            _loopedActionAsync.EndLoop();
        }
    }
}
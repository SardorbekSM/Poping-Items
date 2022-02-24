using System;
using Core.Pool;
using Core.Position;
using Core.Randomizer;
using Core.Spawner.Interfaces;
using Core.WaiterAsync;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using View;
using Object = UnityEngine.Object;

namespace Control
{
    public class ItemController : IStartable, IDisposable
    {
        private readonly IPositionGetter _positionGetter;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private readonly ILoopedAction _loopedAction;
        private readonly IPooler<GameObject> _pooler;
        private readonly GameController _gameController;
        private readonly ItemModel _itemModel;


        public ItemController(IPositionGetter positionGetter, 
            ISpawnerBehaviour spawnerWithPool,  IRandomizer randomizer, LevelModel levelModel,
            GameController gameController, ItemModel itemModel)
        {
            _positionGetter = positionGetter;
            _spawnerWithPool = spawnerWithPool;
            _gameController = gameController;
            _itemModel = itemModel;
            _loopedAction = new LoopedActionAsync();
            _pooler = new RandomizerPooler(_itemModel.Prefabs, randomizer);
            _spawnerWithPool.Initialize(_pooler);
            
            levelModel.Restarted += Start;
            levelModel.LevelCompleted += Dispose;
        }

        public void Start()
        {
            _spawnerWithPool.OnInstantiatedObject += OnSpawned;
            _loopedAction.DoAction += _spawnerWithPool.Spawn;
            _loopedAction.Begin(_itemModel.SpawnDuration);
        }

        private void OnSpawned(GameObject spawnedObject)
        {
            spawnedObject.SetActive(true);

            var itemView = spawnedObject.GetComponent<ItemView>();
            
            Assert.IsNotNull(itemView, "ItemView not found on spawned object in " + spawnedObject);

            var newPattern = _gameController.GetPattern(out var type);

            Assert.IsNotNull(newPattern," Item pattern not created in " + _gameController);

            itemView.ChangePosition(_positionGetter.GetRandom());
            itemView.ChangePattern(newPattern, type);

            var loopedActionAsync = new LoopedActionAsync();
            loopedActionAsync.DoAction += itemView.ResetToDefault;
            loopedActionAsync.Begin(_itemModel.Lifetime);
            itemView.Reseted += () =>
            {
                Object.Destroy(newPattern);
                loopedActionAsync.DoAction -= itemView.ResetToDefault;
                loopedActionAsync.EndLoop();
                spawnedObject.SetActive(false);
                _pooler.Return(spawnedObject);
            };
        }
        
        public void Dispose()
        {
            _spawnerWithPool.OnInstantiatedObject -= OnSpawned;
            _loopedAction.DoAction -= _spawnerWithPool.Spawn;
            _loopedAction.EndLoop();
        }
    }
}
using Control.Interfaces;
using Core.Pool;
using Core.Position;
using Core.Randomizer;
using Core.Spawner.Interfaces;
using Core.WaiterAsync;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using View;

namespace Control
{
    public class ItemController : IItemController
    {
        private readonly IPositionGetter _positionGetter;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private readonly ILoopedAction _loop;
        private readonly IPooler<GameObject> _pooler;
        private readonly GameController _gameController;
        private readonly ItemModel _itemModel;


        public ItemController(IPositionGetter positionGetter, 
            ISpawnerBehaviour spawnerWithPool,  IRandomizer randomizer,
            GameController gameController, ItemModel itemModel)
        {
            _positionGetter = positionGetter;
            _spawnerWithPool = spawnerWithPool;
            _gameController = gameController;
            _itemModel = itemModel;
            _loop = new LoopedActionAsync();
            _pooler = new RandomizerPooler(_itemModel.Prefabs, randomizer);
            _spawnerWithPool.Initialize(_pooler);
        }

        public void StartControl()
        {
            _pooler.Dispose();
            _spawnerWithPool.OnInstantiatedObject += OnSpawned;
            _loop.DoAction += _spawnerWithPool.Spawn;
            _loop.Begin(_itemModel.SpawnDuration);
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
        
        public void EndControl()
        {
            _spawnerWithPool.OnInstantiatedObject -= OnSpawned;
            _spawnerWithPool.Dispose();
            _pooler?.Dispose();
            _loop.DoAction -= _spawnerWithPool.Spawn;
            _loop.EndLoop();
        }
    }
}
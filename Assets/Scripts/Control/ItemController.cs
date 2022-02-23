using Control.Interfaces;
using Core.Position;
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
        private readonly GameController _gameController;
        private readonly ItemModel _itemModel;
        private readonly ILoopedAction _loop;

        public ItemController(IPositionGetter positionGetter, ISpawnerBehaviour spawnerWithPool, GameController gameController, ItemModel itemModel)
        {
            _positionGetter = positionGetter;
            _spawnerWithPool = spawnerWithPool;
            _gameController = gameController;
            _itemModel = itemModel;
            _loop = new LoopedActionAsync();
        }

        public void StartControl()
        {
            _spawnerWithPool.OnInstantiatedObject += OnSpawned;
            _spawnerWithPool.Initialize(_itemModel.Prefabs);
            _loop.DoAction += _spawnerWithPool.Spawn;
            _loop.Begin(_itemModel.SpawnDuration);
        }

        private void OnSpawned(GameObject obj)
        {
            var item = obj.GetComponent<ItemView>();
            
            Assert.IsNotNull(item, "ItemView not found on spawned object in " + obj);

            var newPattern = _gameController.GetPattern(out var type);

            Assert.IsNotNull(newPattern," Item pattern not created in " + _gameController);

            item.ChangePosition(_positionGetter.GetRandom());
            item.ChangePattern(newPattern, type);

            var loopedActionAsync = new LoopedActionAsync();
            loopedActionAsync.DoAction += item.ResetToDefault;
            loopedActionAsync.Begin(_itemModel.Lifetime);
            item.Reseted += () =>
            {
                loopedActionAsync.DoAction -= item.ResetToDefault;
                loopedActionAsync.EndLoop();
            };
        }
        
        public void EndControl()
        {
            _spawnerWithPool.OnInstantiatedObject -= OnSpawned;
            _spawnerWithPool.Dispose();
            _loop.DoAction -= _spawnerWithPool.Spawn;
            _loop.EndLoop();
        }
    }
}
using Core.Spawner;
using Core.Spawner.Interfaces;
using Core.WaiterAsync;
using Data;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using View;

namespace Control
{
    public class ItemController
    {
        private readonly IPositionGetter _positionGetter;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private readonly PatternModel _patternModel;

        private readonly ItemModel _itemModel;
        private LoopedActionAsync _loopedActionAsync; // Не инжектится на VContainer

        public ItemController(IPositionGetter positionGetter, ISpawnerBehaviour spawnerWithPool, PatternModel patternModel, ItemModel itemModel)
        {
            _positionGetter = positionGetter;
            _spawnerWithPool = spawnerWithPool;
            _patternModel = patternModel;
            _itemModel = itemModel;
        }

        public void StartControl()
        {
            _spawnerWithPool.OnInstantiatedObject += OnSpawned;
            _spawnerWithPool.Dispose();
            _itemModel.BeginSpawn();
        }

        private void OnSpawned(GameObject obj)
        {
            var item = obj.GetComponent<ItemView>();
            
            Assert.IsNotNull(item, "ItemView not found on spawned object in " + obj);

            var newPattern = _patternModel.GetPattern(out var type);

            Assert.IsNotNull(newPattern," Item pattern not created in " + _patternModel);

            // Две методы всегда вызываются одновременно
            item.ChangePosition(_positionGetter.GetRandom());
            item.ChangePattern(newPattern, type);
        }
        
        public void EndControl()
        {
            _spawnerWithPool.OnInstantiatedObject -= OnSpawned;
            _itemModel.EndSpawn();
        }
    }
}
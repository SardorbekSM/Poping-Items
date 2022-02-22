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
        private readonly PatternGenerator _patternGenerator;
        private readonly ItemModel _itemModel;
        
        public ItemController(IPositionGetter positionGetter, ISpawnerBehaviour spawnerWithPool, PatternGenerator patternGenerator, ItemModel itemModel)
        {
            _positionGetter = positionGetter;
            _spawnerWithPool = spawnerWithPool;
            _patternGenerator = patternGenerator;
            _itemModel = itemModel;
        }

        public void StartControl()
        {
            _spawnerWithPool.OnInstantiatedObject += OnSpawned;
            _spawnerWithPool.Dispose();
            _spawnerWithPool.Initialize(_itemModel.Prefabs, _itemModel.SpawnDuration);
        }

        private void OnSpawned(GameObject obj)
        {
            var item = obj.GetComponent<ItemView>();
            
            Assert.IsNotNull(item, "ItemView not found on spawned object in " + obj);

            var newPattern = _patternGenerator.GetPattern(out var type);

            Assert.IsNotNull(newPattern," Item pattern not created in " + _patternGenerator);

            item.ChangePosition(_positionGetter.GetRandom());
            item.ChangePattern(newPattern, type);
        }
        
        public void EndControl()
        {
            _spawnerWithPool.OnInstantiatedObject -= OnSpawned;
            _spawnerWithPool.Dispose();
        }
    }
}
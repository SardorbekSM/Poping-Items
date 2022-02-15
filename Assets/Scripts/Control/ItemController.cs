using Core.Spawner;
using Core.Spawner.Interfaces;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using View;

namespace Control
{
    public class ItemController : IStartable
    {
        private readonly IPositionGetter _positionGetter;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private readonly ItemModel _itemModel;

        public ItemController(IPositionGetter positionGetter, ISpawnerBehaviour spawnerWithPool, ItemModel itemModel)
        {
            _positionGetter = positionGetter;
            _spawnerWithPool = spawnerWithPool;
            _itemModel = itemModel;
        }

        public void Start()
        {
            _spawnerWithPool.OnInstantiatedObject += OnSpawned;
        }

        private void OnSpawned(GameObject obj)
        {
            var item = obj.GetComponent<ItemView>();
            
            Assert.IsNotNull(item);
            item.ChangePattern(_itemModel.GetPattern());

            item.ChangePosition(_positionGetter.GetRandom());
        }
    }
}
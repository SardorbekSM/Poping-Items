using Core.Spawner;
using Core.Spawner.Interfaces;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using View;

namespace Control
{
    public class ItemControl : IStartable
    {
        private readonly IPositionGetter _positionGetter;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        
        public ItemControl(IPositionGetter positionGetter, ISpawnerBehaviour spawnerWithPool)
        {
            _positionGetter = positionGetter;
            _spawnerWithPool = spawnerWithPool;
        }

        public void Start()
        {
            _spawnerWithPool.OnInstantiatedObject += OnSpawned;
        }

        private void OnSpawned(GameObject obj)
        {
            var item = obj.GetComponent<ItemView>();
            
            Assert.IsNotNull(item);
            
            item.ChangePosition(
                _positionGetter.GetRandom());
        }
    }
}
using Core.Spawner;
using Model;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Control
{
    public class ItemControl : IStartable
    {
        private readonly IPositionGetter _positionGetter;
        private readonly SpawnerWithPool _spawnerWithPool;
        
        public ItemControl(IPositionGetter positionGetter, SpawnerWithPool spawnerWithPool)
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
            item.ChangePosition(
                _positionGetter.GetRandom());
        }
    }
}
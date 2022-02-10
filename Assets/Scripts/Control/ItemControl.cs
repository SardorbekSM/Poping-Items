using Core.Spawner;
using Model;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Control
{
    public class ItemControl : IStartable
    {
        private readonly IPosition _position;
        private readonly SpawnerWithPool _spawnerWithPool;
        
        public ItemControl(IPosition position, SpawnerWithPool spawnerWithPool)
        {
            _position = position;
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
                _position.GetRandom());
        }
    }
}
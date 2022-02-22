using Core.Spawner.Interfaces;
using Core.WaiterAsync;
using Data;

namespace Model
{
    public class ItemModel
    {
        private readonly ItemsData _itemsData;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private ILoopedAction _loopedActionAsync;

        public ItemModel(ItemsData itemsData, ISpawnerBehaviour spawnerBehaviour)
        {
            _itemsData = itemsData;
            _spawnerWithPool = spawnerBehaviour;
        }

        public void BeginSpawn()
        {
            _loopedActionAsync = new LoopedActionAsync();
            _loopedActionAsync.DoAction += _spawnerWithPool.Spawn;
            _loopedActionAsync.Begin(_itemsData.SpawnDuration);
        }
        
        public void EndSpawn()
        {
            _loopedActionAsync.DoAction -= _spawnerWithPool.Spawn;
            _loopedActionAsync.EndLoop();
        }
    }
}
using Data;

namespace Model
{
    public class ItemModel
    {
        private readonly ItemsData _itemsData;

        public float SpawnDuration => _itemsData.SpawnDuration;
        public SpawnData Prefabs => _itemsData.Prefabs;
        public float Lifetime => _itemsData.ItemsLifeTime;

        public ItemModel(ItemsData itemsData)
        {
            _itemsData = itemsData;
        }
    }
}
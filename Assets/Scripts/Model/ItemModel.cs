using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Model
{
    public class ItemModel
    {
        private readonly ItemsData _itemsData;

        public float SpawnDuration => _itemsData.SpawnDuration;
        public IEnumerable<GameObject> Prefabs => _itemsData.Items;
        public float Lifetime => _itemsData.ItemsLifeTime;

        public ItemModel(ItemsData itemsData)
        {
            _itemsData = itemsData;
        }
    }
}
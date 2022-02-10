using Data;
using UnityEngine;
using UnityEngine.Assertions;

namespace Model
{
    public class SpawnModel
    {
        public int SpawnCount => _spawnData.SpawnCount;
        public float SpawnDuration => _spawnData.SpawnDuration;
        public float LifeTime => _spawnData.LifeTime;

        public GameObject[] Prefabs => _spawnData.Prefabs;

        public float LeftBorder { get; private set; }
        public float RightBoder { get; private set; }
        public float BottomBorder { get; private set; }

        private readonly SpawnData _spawnData;
        private readonly ItemsOffsetData _itemsOffsetData;
        private readonly Camera _mainCamera;

        public SpawnModel(ItemsOffsetData itemsOffsetData, SpawnData spawnData, Camera mainCamera)
        {
            _spawnData = spawnData;
            _itemsOffsetData = itemsOffsetData;
            _mainCamera = mainCamera;
            CalculateBorders();
        }

        private void CalculateBorders()
        {
            Assert.IsNotNull(_mainCamera);
        
            var leftBottomCorner = _mainCamera.ScreenToWorldPoint(Vector2.zero);
            var rightTopCorner = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            LeftBorder = leftBottomCorner.x + _itemsOffsetData.LeftOffset;
            RightBoder = rightTopCorner.x + _itemsOffsetData.RightOffset;
            BottomBorder = leftBottomCorner.y + _itemsOffsetData.BottomOffset;
        }
    }
}
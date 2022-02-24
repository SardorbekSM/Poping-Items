using UnityEngine;
using UnityEngine.Assertions;

using Data;

namespace Core.CameraUtils
{
    public class MainCameraBorders
    {
        private readonly ItemsOffsetData _itemsOffsetData;
        private readonly Camera _mainCamera;

        public float LeftBorder { get; private set; }
        public float RightBorder { get; private set; }
        public float BottomBorder { get; private set; }

        public MainCameraBorders(ItemsOffsetData itemsOffsetData, Camera mainCamera)
        {
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
            RightBorder = rightTopCorner.x + _itemsOffsetData.RightOffset;
            BottomBorder = leftBottomCorner.y + _itemsOffsetData.BottomOffset;
        }
    }
}
using System;

using UnityEngine;
using Random = UnityEngine.Random;

using Core.CameraUtils;
using Data;

namespace Core.Position
{
    public class PositionGetter : IPositionGetter
    {
        private readonly MainCameraBorders _cameraBorders;
        private readonly ItemsOffsetData _itemsOffsetData;

        private float _lastXPosition;

        public PositionGetter(MainCameraBorders cameraBorders, ItemsOffsetData itemsOffsetData)
        {
            _cameraBorders = cameraBorders;
            _itemsOffsetData = itemsOffsetData;
        }
        
        public Vector2 GetRandom()
        {
            return GetRandomInCamera(_cameraBorders.LeftBorder, _cameraBorders.RightBorder, _cameraBorders.BottomBorder);
        }

        private Vector2 GetRandomInCamera(float minX, float maxX, float bottom)
        {
            var xPosition = _lastXPosition;
            
            while (Math.Abs(xPosition - _lastXPosition) < _itemsOffsetData.BetweenDistance)
            {
                xPosition = Random.Range(minX, maxX);
            }

            _lastXPosition = xPosition;
            
            return new Vector2( xPosition, bottom);
        }
    }
}

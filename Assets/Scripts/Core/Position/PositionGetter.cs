using System;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Position
{
    public class PositionGetter : IPositionGetter
    {
        private readonly SpawnModel _spawnModel;

        private float _lastXPosition;
        
        public PositionGetter(SpawnModel spawnModel)
        {
            _spawnModel = spawnModel;
        }
        
        public Vector2 GetDefault()
        {
            return Vector2.zero;
        }

        public Vector2 GetRandom()
        {
            return GetRandomInCamera(_spawnModel.LeftBorder, _spawnModel.RightBorder, _spawnModel.BottomBorder);
        }

        private Vector2 GetRandomInCamera(float minX, float maxX, float bottom)
        {
            var xPosition = _lastXPosition;
            
            while (Math.Abs(xPosition - _lastXPosition) < 2)
            {
                xPosition = Random.Range(minX, maxX);
            }

            _lastXPosition = xPosition;
            
            return new Vector2( xPosition, bottom);
        }
    }
}

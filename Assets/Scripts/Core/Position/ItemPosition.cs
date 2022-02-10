using Model;
using UnityEngine;

namespace Core.Position
{
    public class ItemPosition : IPosition
    {
        private readonly SpawnModel _spawnModel;
        
        public ItemPosition(SpawnModel spawnModel)
        {
            _spawnModel = spawnModel;
        }
        
        public Vector2 GetDefault()
        {
            return Vector2.zero;
        }

        public Vector2 GetRandom()
        {
            return GetRandomInCamera(_spawnModel.LeftBorder, _spawnModel.RightBoder, _spawnModel.BottomBorder);
        }

        private Vector2 GetRandomInCamera(float minX, float maxX, float bottom)
        {
            return new Vector2(Random.Range(minX, maxX), bottom);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Pool
{
    public class Pooler : IPooler<GameObject>
    {
        private readonly IList<GameObject> _returnedObjects;
        private readonly IList<GameObject> _poolingObjects;

        private int _currentObjectIndex = -1;
        
        private readonly IFactoryGameObject<GameObject> _factory;

        protected Pooler(IEnumerable<GameObject> poolingObjects)
        {
            _returnedObjects = new List<GameObject>();
            _poolingObjects = new List<GameObject>(poolingObjects);
            _factory = new GameObjectFactory();
        }

#pragma warning disable CS1998
        public async UniTask<GameObject> Get()
#pragma warning restore CS1998
        {
            if (_returnedObjects.Count > 0)
            {
                var item = _returnedObjects[0];
                _returnedObjects.Remove(item);
                return item;
            }

            var one = _factory.Create(_poolingObjects[GetIndex(_poolingObjects.Count())]);

            return one;
        }

        public void Return(GameObject item)
        {
            if (_returnedObjects.Contains(item)) return;

            _returnedObjects.Add(item);
        }

        protected virtual int GetIndex(int maxValue)
        {
            _currentObjectIndex++;
            
            if (_currentObjectIndex >= maxValue)
                _currentObjectIndex = 0;
            
            return _currentObjectIndex;
        }

        public void Dispose()
        {
            _returnedObjects.Clear();
        }
    }
}

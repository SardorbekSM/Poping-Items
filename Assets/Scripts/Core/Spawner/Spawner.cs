using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Core.Spawner
{
    public class Spawner <T> : ISpawner<T> where T : Object
    {
        private readonly IPooler<T> _pooler;

        private bool _isSpawning;

        public Spawner(IPooler<T> pooler)
        {
            _pooler = pooler;
        }

        public bool IsSpawning()
        {
            return _isSpawning;
        }

        public async UniTask BeginSpawning(Action<T> onInstantiatedObject)
        {
            _isSpawning = true;

            var pooledObject = await _pooler.Get();

            onInstantiatedObject?.Invoke(pooledObject);

            _isSpawning = false;
        }
    }
}

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class Pooler : IPooler<GameObject>
{
    private readonly List<GameObject> _returnedObjects;
    private readonly GameObject[] _poolingObjects;

    private int _currentObjectIndex = -1;
        
    private readonly IFactoryGameObject<GameObject> _factory;

    public Pooler(GameObject[] poolingObjects)
    {
        _returnedObjects = new List<GameObject>();
        _poolingObjects = poolingObjects;
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

        var one = _factory.Create(_poolingObjects[GetIndex(_poolingObjects.Length)]);

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

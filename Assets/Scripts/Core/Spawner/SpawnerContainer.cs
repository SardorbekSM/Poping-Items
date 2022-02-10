using System.Collections.Generic;

using UnityEngine;

public class SpawnerContainer<T> : ISpawnerContainer<T> where T : Object
{
    private readonly List<T> _spawnedObjects;

    public SpawnerContainer()
    {
        _spawnedObjects = new List<T>(10);
    }
        
    public void SetValue(T value)
    {
        if (!_spawnedObjects.Contains(value))
        {
            _spawnedObjects.Add(value);
        }
    }

    public void Dispose()
    {
        var count = _spawnedObjects.Count;

        for (var i = 0; i < count; i++)
        {
            Object.Destroy(_spawnedObjects[i]);
        }
            
        _spawnedObjects.Clear();
    }

    public void DestroySpawnedObject(T spawnedObject)
    {
        if (_spawnedObjects.Remove(spawnedObject))
        {
            Object.Destroy(spawnedObject);
        }
    }

    public List<T> GetSpawnedObjects()
    {
        return _spawnedObjects;
    }
}

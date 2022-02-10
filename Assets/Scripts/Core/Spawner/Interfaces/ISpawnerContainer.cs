using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnerContainer<T> : IValueSetter<T>, IDisposable
{
    void DestroySpawnedObject(T spawnedObject);

    List<T> GetSpawnedObjects();
}

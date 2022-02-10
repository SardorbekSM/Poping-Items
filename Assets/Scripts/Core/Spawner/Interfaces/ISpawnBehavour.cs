using System;
using UnityEngine;

public interface ISpawnerBehaviour : IDisposable
{
    void Spawn(int count);

    void OnSpawnedObject(GameObject spawnedGameObject);
}
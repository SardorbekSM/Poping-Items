using System;
using UnityEngine;

public interface ISpawnerBehaviour : IDisposable
{
    void Spawn();

    void OnSpawnedObject(GameObject spawnedGameObject);
}
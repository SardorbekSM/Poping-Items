using System;
using UnityEngine;

namespace Core.Spawner.Interfaces
{
    public interface ISpawnerBehaviour : IDisposable
    {
        event Action<GameObject> OnInstantiatedObject;
        void Spawn();

        void OnSpawnedObject(GameObject spawnedGameObject);
    }
}
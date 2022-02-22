using System;
using Data;
using UnityEngine;

namespace Core.Spawner.Interfaces
{
    public interface ISpawnerBehaviour : IDisposable
    {
        event Action<GameObject> OnInstantiatedObject;
        void Initialize(SpawnData spawnData, float duration);

        void OnSpawnedObject(GameObject spawnedGameObject);
    }
}
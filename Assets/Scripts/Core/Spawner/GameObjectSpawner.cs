using System;
using Core.Spawner.Interfaces;
using Data;
using UnityEngine;

namespace Core.Spawner
{
    public class GameObjectSpawner : ISpawnerBehaviour
    {
        public event Action<GameObject> OnInstantiatedObject;
        public void Initialize(SpawnData spawnData, float duration)
        {
            throw new NotImplementedException();
        }

        public void OnSpawnedObject(GameObject spawnedGameObject)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
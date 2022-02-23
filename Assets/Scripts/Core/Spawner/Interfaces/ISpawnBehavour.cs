using System;
using Data;
using UnityEngine;

namespace Core.Spawner.Interfaces
{
    public interface ISpawnerBehaviour : IDisposable
    {
        event Action<GameObject> OnInstantiatedObject;
        void Initialize(IPooler<GameObject> pooler);

        void Spawn();
    }
}
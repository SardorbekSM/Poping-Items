using System;

public interface ISpawnerBehaviour<T> : IDisposable
{
    void Spawn(int count);

    void OnSpawnedObject(T spawnedGameObject);

    void DestroySpawnedObject(T spawnedGameObject);
}
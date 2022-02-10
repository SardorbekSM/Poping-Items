using System;
using Cysharp.Threading.Tasks;

public interface ISpawner<T>
{
    bool IsSpawning();

    UniTask BeginSpawning(Action<T> onInstantiatedObject);
}
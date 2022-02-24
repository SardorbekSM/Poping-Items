using System;

using Cysharp.Threading.Tasks;

public interface IPooler<T> : IDisposable
{
    UniTask<T> Get();

    void Return(T item);
}
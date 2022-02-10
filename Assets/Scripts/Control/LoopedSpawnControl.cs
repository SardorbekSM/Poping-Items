using System;
using System.Collections;
using System.Threading;
using Core.Spawner;
using Cysharp.Threading.Tasks;
using Model;
using UnityEngine;

using VContainer;

public class LoopedSpawnControl : MonoBehaviour
{
    private SpawnModel _spawnModel;
    private SpawnerWithPool _spawnerWithPool;
    private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

    [Inject]
    public void Inject(SpawnerWithPool spawnerWithPool, SpawnModel spawnModel)
    {
        _spawnerWithPool = spawnerWithPool;
        _spawnModel = spawnModel;
    }

    private Coroutine _loopingCoroutine;
    
    public void Start()
    {
        StopLooping();
        
        _loopingCoroutine = StartCoroutine(Looping());

        Begin(_cancellation.Token);
    }

    public void StopLooping()
    {
        if(_loopingCoroutine == null) return;
            
        StopCoroutine(_loopingCoroutine);
        _loopingCoroutine = null;
        _cancellation.Dispose();
    }

    private IEnumerator Looping()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(_spawnModel.SpawnDuration);
            
            _spawnerWithPool.Spawn(_spawnModel.SpawnCount);
        }
            
        _loopingCoroutine = null;
    }

    private async UniTask Begin(CancellationToken token = default)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_spawnModel.LifeTime), cancellationToken: token);
        StopLooping();
    }
}

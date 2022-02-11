using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.WaiterAsync
{
    public class LoopedActionAsync
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellation;
        private UniTask _currentProcess;
        
        public event Action DoAction;
        private bool _isLooped;
        
        public LoopedActionAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellation = _cancellationTokenSource.Token;
        }

        public void Begin(float duration)
        {
            _currentProcess = BeginLoop(duration);
        }

        private async UniTask BeginLoop(float duration)
        {
            _isLooped = true;
            
            while (_isLooped)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: _cancellation);
                DoAction?.Invoke();
            }
        }

        public void EndLoop()
        {
            _isLooped = false;
            _cancellationTokenSource.Dispose();
            _currentProcess = default;
        }
    }
}

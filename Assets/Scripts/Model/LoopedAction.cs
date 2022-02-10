using System;
using Cysharp.Threading.Tasks;

namespace Model
{
    public class LoopedAction
    {
        public event Action DoAction;
        public bool IsLooped { get; set; }
    
        public async UniTask BeginLoop(float duration)
        {
            while (IsLooped)
            {
                DoAction?.Invoke();
                await UniTask.Yield();
            }
        }
    }
}

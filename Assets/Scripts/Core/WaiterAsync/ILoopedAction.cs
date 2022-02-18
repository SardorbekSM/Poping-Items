using System;

namespace Core.WaiterAsync
{
    public interface ILoopedAction
    {
        event Action DoAction;
        void Begin(float duration);
        void EndLoop();
    }
}
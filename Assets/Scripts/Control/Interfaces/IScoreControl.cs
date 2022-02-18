using System;

namespace Control.Interfaces
{
    public interface IScoreControl : IControl
    {// Для других скриптов тоже создать интерфейсы?
        event Action Scored;
        float AddScore();
    }
}
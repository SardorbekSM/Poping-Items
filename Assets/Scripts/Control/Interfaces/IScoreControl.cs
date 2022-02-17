using System;

namespace Control.Interfaces
{
    public interface IScoreControl
    {// Для других скриптов тоже создать интерфейсы?
        event Action Scored;
        float AddScore();
    }
}
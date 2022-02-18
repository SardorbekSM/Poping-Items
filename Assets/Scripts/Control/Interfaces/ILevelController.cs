using System;

namespace Control.Interfaces
{
    public interface ILevelController : IControl
    {// Для других скриптов тоже создать интерфейсы?
        event Action Scored;
        float AddScore();
    }
}
using System;

namespace Control.Interfaces
{
    public interface ILevelController : IControl
    {
        event Action Scored;
        float AddScore();
    }
}
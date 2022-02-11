using System;

namespace Control.Interfaces
{
    public interface IScoreControl
    {
        event Action Scored;
        float AddScore();
        void OnScoreChanged(float score);
    }
}
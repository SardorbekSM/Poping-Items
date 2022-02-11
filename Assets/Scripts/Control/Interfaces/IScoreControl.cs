using System;

namespace Control.Interfaces
{
    public interface IScoreControl
    {
        float AddScore();
        void OnScoreChanged(float score);
    }
}
using System;
using Data;

namespace Model
{
    public class LevelModel
    {
        private readonly LevelData _levelData;
        
        public int StartValue => _levelData.StartValue;
        public int LevelItemsCount => _levelData.LevelRequiredItemsCount;
        public int IterationItemsCount => _levelData.IterationRequiredItemsCount;

        public int IterationScore { get; private set; }

        public int LevelScore { get; private set; }

        public event Action ScoreChanged;
        public Action iterationCompleted;
        public Action levelCompleted;
        public Action restarted;

        public void IncrementScore()
        {
            ++LevelScore;
            ++IterationScore;
            ScoreChanged?.Invoke();
        }

        public void ResetIterationScore()
        {
            IterationScore = StartValue;
            iterationCompleted?.Invoke();
        }

        public void ResetLevelScore()
        {
            LevelScore = StartValue;
            levelCompleted?.Invoke();
        }

        public LevelModel(LevelData levelData)
        {
            _levelData = levelData;
        }
    }
}
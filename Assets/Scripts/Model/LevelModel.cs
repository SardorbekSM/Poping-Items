using System;
using Data;
using UnityEngine;

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
        public Action IterationCompleted;
        public Action LevelCompleted;
        public Action Restarted;

        public void IncrementScore()
        {
            ++LevelScore;
            ++IterationScore;
            Debug.Log("Score Changed");
            ScoreChanged?.Invoke();
        }

        public void ResetIterationScore()
        {
            IterationScore = StartValue;
        }

        public void ResetLevelScore()
        {
            LevelScore = StartValue;
        }

        public LevelModel(LevelData levelData)
        {
            _levelData = levelData;
        }
    }
}
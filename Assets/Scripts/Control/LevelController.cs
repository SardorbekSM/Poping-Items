using System;

using Control.Interfaces;
using Model;

namespace Control
{
    public class LevelController : IControl
    {
        private readonly LevelModel _levelModel;
        private readonly PatternModel _patternModel;

        private int _score;
        private int _iterationScore;
        
        public event Action Scored;

        public LevelController(LevelModel levelModel, PatternModel patternModel)
        {
            _levelModel = levelModel;
            _patternModel = patternModel;
        }

        public void StartControl()
        {
            _patternModel.Initialize();
        }

        public float AddScore()
        {
            var newScore = ++_score;
            
            OnScoreChanged();
            
            return newScore;
        }

        private void OnScoreChanged()
        {
            _iterationScore++;

            if (_iterationScore < _levelModel.IterationItemsCount) return;

            _iterationScore = _levelModel.StartValue;

            _patternModel.Initialize();

            if (_score < _levelModel.LevelItemsCount) return;
            
            AllLevelsComplete();
        }

        private void AllLevelsComplete()
        {
            _score = _levelModel.StartValue;
            Scored?.Invoke();
        }

        public void EndControl()
        {
            _patternModel.ResetToDefault();
        }
    }
}

using System;

using Control.Interfaces;
using Model;

namespace Control
{
    public class LevelController : ILevelController
    {
        private readonly LevelModel _levelModel;
        private readonly PatternGenerator _patternGenerator;

        private int _score;
        private int _iterationScore;
        
        public event Action Scored;

        public LevelController(LevelModel levelModel, PatternGenerator patternGenerator)
        {
            _levelModel = levelModel;
            _patternGenerator = patternGenerator;
        }

        public void StartControl()
        {
            _patternGenerator.Initialize();
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

            _patternGenerator.Initialize();

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
            _patternGenerator.ResetToDefault();
        }
    }
}

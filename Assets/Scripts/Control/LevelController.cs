using System;

using Control.Interfaces;
using Model;

namespace Control
{
    public class LevelController : IScoreControl
    {
        private readonly SliderModel _sliderModel;
        private readonly LevelModel _levelModel;
        private readonly PatternModel _patternModel;

        private float _score;
        
        public event Action Scored;

        public LevelController(SliderModel sliderModel, LevelModel levelModel, PatternModel patternModel)
        {
            _sliderModel = sliderModel;
            _levelModel = levelModel;
            _patternModel = patternModel;
            _levelModel.AllLevelsCompleted += AllLevelsComplete;
        }

        public float AddScore()
        {
            var newScore = _score += _sliderModel.Step;
            
            OnScoreChanged();
            
            return newScore;
        }

        private void OnScoreChanged()
        {
            if (_score < _sliderModel.FillMax) return;
            _score = _sliderModel.FillMin;
            _levelModel.InitializeNewPatterns();
            _patternModel.Initialize();
        }

        private void AllLevelsComplete()
        {
            Scored?.Invoke();
        }
    }
}

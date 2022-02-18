using System;

using Control.Interfaces;
using Model;

namespace Control
{
    public class LevelController : IControl
    {
        private readonly SliderModel _sliderModel;
        private readonly LevelModel _levelModel;
        private readonly PatternModel _patternModel;

        private int _score;
        private int _iterationScore;
        
        public event Action Scored;

        public LevelController(SliderModel sliderModel, LevelModel levelModel, PatternModel patternModel)
        {
            _sliderModel = sliderModel;
            _levelModel = levelModel;
            _patternModel = patternModel;
        }

        public void StartControl()
        {
            _levelModel.InitializeNewPatterns();
            _patternModel.Initialize();
        }

        public float AddScore()
        {
            var newScore = _score += _sliderModel.Step;
            
            OnScoreChanged();
            
            return newScore;
        }

        private void OnScoreChanged()
        {
            _iterationScore++;

            if (_iterationScore < _sliderModel.RequiredItemsCount) return;

            _iterationScore = _sliderModel.FillMin;

            _levelModel.InitializeNewPatterns();
            _patternModel.Initialize();

            if (_score < _sliderModel.FillMax) return;
            
            AllLevelsComplete();
        }

        private void AllLevelsComplete()
        {
            _score = _sliderModel.FillMin;
            Scored?.Invoke();
        }

        public void EndControl()
        {
            _patternModel.ResetToDefault();
            _levelModel.ResetToDefault();
        }
    }
}

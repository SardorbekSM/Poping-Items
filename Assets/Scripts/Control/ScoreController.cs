using System;

using Control.Interfaces;
using Model;

namespace Control
{
    public class ScoreController : IScoreControl
    {
        private readonly SliderModel _sliderModel;

        private float _score;
        
        public event Action Scored;

        public ScoreController(SliderModel sliderModel)
        {
            _sliderModel = sliderModel;
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
            Scored?.Invoke();
        }
    }
}

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
            return _score += _sliderModel.Step;
        }

        public void OnScoreChanged(float score)
        {
            if (!(score >= _sliderModel.FillMax)) return;
            _score = _sliderModel.FillMin;
            Scored?.Invoke();
        }
    }
}

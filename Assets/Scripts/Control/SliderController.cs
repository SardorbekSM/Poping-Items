using System;

using VContainer.Unity;

using Model;
using View;

namespace Control
{
    public class SliderController : IStartable, IDisposable
    {
        private readonly SliderView _sliderView;
        private readonly LevelModel _levelModel;

        public SliderController(SliderView sliderView, LevelModel levelModel)
        {
            _sliderView = sliderView;
            _levelModel = levelModel;
            levelModel.restarted += Start;
            levelModel.levelCompleted += Dispose;
        }

        public void Start()
        {
            _sliderView.CreateSliderSpan(_levelModel.StartValue, _levelModel.LevelItemsCount);
            _levelModel.ScoreChanged += ChangeSliderValue;
        }
        
        private void ChangeSliderValue()
        {
            _sliderView.ChangeValue(_levelModel.LevelScore);
        }

        public void Dispose()
        {
            _levelModel.ScoreChanged -= ChangeSliderValue;
        }
    }
}
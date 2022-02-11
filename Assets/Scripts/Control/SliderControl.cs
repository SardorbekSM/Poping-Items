using System;
using Control.Interfaces;
using Core.Spawner;
using Core.Spawner.Interfaces;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using View;

namespace Control
{
    public class SliderControl
    {
        private readonly SliderView _sliderView;
        private readonly SliderModel _sliderModel;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private readonly IScoreControl _scoreControl;

        public SliderControl(SliderView sliderView, SliderModel sliderModel, ISpawnerBehaviour spawnerWithPool, IScoreControl scoreControl)
        {
            _sliderView = sliderView;
            _sliderModel = sliderModel;
            _spawnerWithPool = spawnerWithPool;
            _scoreControl = scoreControl;
        }

        public void StartControl()
        {
            _spawnerWithPool.OnInstantiatedObject += SubscribeToClick;
            _sliderView.ChangeSliderMinCount(_sliderModel.FillMin);
            _sliderView.ChangeSliderMaxCount(_sliderModel.FillMax);
            _sliderView.ChangeValue(_sliderModel.FillMin);
        }

        private void SubscribeToClick(GameObject obj)
        {
            var item = obj.GetComponent<IClickBehaviour>();
            
            Assert.IsNotNull(item);

            item.ButtonClicked += ChangeSliderValue;
        }
        
        private void ChangeSliderValue()
        {
            _sliderView.ChangeValue(_scoreControl.AddScore());
        }

        public void EndControl()
        {
            _spawnerWithPool.OnInstantiatedObject -= SubscribeToClick;
        }
    }
}
﻿using System;
using Control.Interfaces;
using Core.Spawner;
using Core.Spawner.Interfaces;
using Model;
using UnityEngine;
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
        }

        private void SubscribeToClick(GameObject obj)
        {
            var click = obj.GetComponent<IClickBehaviour>();
            click.ButtonClicked += SliderValue;
        }

        private void SliderValue()
        {
            _sliderView.ChangeValue(_scoreControl.AddScore());
        }

        public void EndControl()
        {
            _spawnerWithPool.OnInstantiatedObject -= SubscribeToClick;
            
        }
    }
}
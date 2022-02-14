﻿using System;
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
    public class SliderController
    {
        private readonly SliderView _sliderView;
        private readonly SliderModel _sliderModel;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private readonly IScoreControl _scoreControl;

        public SliderController(SliderView sliderView, SliderModel sliderModel, ISpawnerBehaviour spawnerWithPool, IScoreControl scoreControl)
        {
            _sliderView = sliderView;
            _sliderModel = sliderModel;
            _spawnerWithPool = spawnerWithPool;
            _scoreControl = scoreControl;
        }

        public void StartControl()
        {
            _spawnerWithPool.OnInstantiatedObject += SubscribeToClick;
            _sliderView.CreateSliderSpan(_sliderModel.FillMin, _sliderModel.FillMax);
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
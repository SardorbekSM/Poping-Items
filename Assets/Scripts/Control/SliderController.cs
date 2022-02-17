using Control.Interfaces;
using Core;
using Core.Spawner.Interfaces;
using Data;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using View;

namespace Control
{
    public class SliderController
    {
        private readonly SliderView _sliderView;
        private readonly SliderModel _sliderModel;
        private readonly IScoreControl _scoreControl;
        private readonly ISpawnerBehaviour _spawnerBehaviour;

        public SliderController(SliderView sliderView, SliderModel sliderModel, IScoreControl scoreControl, ISpawnerBehaviour spawnerBehaviour)
        {
            _sliderView = sliderView;
            _sliderModel = sliderModel;
            _scoreControl = scoreControl;
            _spawnerBehaviour = spawnerBehaviour;
        }

        public void StartControl()
        {
            _spawnerBehaviour.OnInstantiatedObject += SubscribeToClick;
            _sliderView.CreateSliderSpan(_sliderModel.FillMin, _sliderModel.FillMax);
        }

        private void SubscribeToClick(GameObject obj)
        {
            var item = obj.GetComponent<IClickBehaviour>();
            
            Assert.IsNotNull(item);

            item.ButtonClicked += ChangeSliderValue;
        }
        
        private void ChangeSliderValue(GameObject obj)
        {
            var view = obj.GetComponent<ItemView>();
            
            Assert.IsNotNull(view); 

            if (view.PatternType != InteractableType.Correct) return;
            
            _sliderView.ChangeValue(_scoreControl.AddScore());
            Debug.Log("Add Score");
        }

        public void EndControl()
        {
            _spawnerBehaviour.OnInstantiatedObject -= SubscribeToClick;
        }
    }
}
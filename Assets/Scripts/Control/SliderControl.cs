using Core.Spawner;
using Model;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Control
{
    public class SliderControl : IStartable
    {
        private readonly SliderView _sliderView;
        private readonly SliderModel _sliderModel;
        private readonly SpawnerWithPool _spawnerWithPool;

        public SliderControl(SliderView sliderView, SliderModel sliderModel, SpawnerWithPool spawnerWithPool)
        {
            _sliderView = sliderView;
            _sliderModel = sliderModel;
            _spawnerWithPool = spawnerWithPool;
        }

        public void Start()
        {
            _spawnerWithPool.OnInstantiatedObject += SubscribeToClick;
            _sliderView.ChangeSliderMaxCount(_sliderModel.FillMax);
        }

        private void SubscribeToClick(GameObject obj)
        {
            var click = obj.GetComponent<IClickBehaviour>();
            click.ButtonClicked += AddFillValue;
        }

        private void AddFillValue()
        {
            _sliderView.ChangeValue(_sliderModel.Step);
        }
    }
}
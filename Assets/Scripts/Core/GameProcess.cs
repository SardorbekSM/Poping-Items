using System;
using Control;
using Control.Interfaces;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Core
{
    public class GameProcess : IStartable, IDisposable
    {
        private readonly SliderController _sliderController;
        private readonly LevelController _levelController;
        private readonly EndGameView _endGameView;
        private readonly ItemController _itemController;

        public GameProcess(
            SliderController sliderController, 
            LevelController levelController, 
            EndGameView endGameView,
            ItemController itemController
            )
        {
            _sliderController = sliderController;
            _levelController = levelController;
            _endGameView = endGameView;
            _itemController = itemController;
        }

        public void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _itemController.StartControl();
            _sliderController.StartControl();
            _levelController.StartControl();
            _levelController.Scored += End;
        }

        private void RestartGame()
        {
            Initialize();
        }

        private void End()
        {
            _endGameView.Activate(RestartGame);
            Dispose();
        }

        public void Dispose()
        {
            _itemController.EndControl();
            _sliderController.EndControl();
            _levelController.EndControl();
            _levelController.Scored -= End;
        }
    }
}
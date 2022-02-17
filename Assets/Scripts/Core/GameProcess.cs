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
        private readonly IScoreControl _scoreControl;
        private readonly EndGameView _endGameView;
        private readonly ItemController _itemController;

        public GameProcess(
            SliderController sliderController, 
            IScoreControl scoreControl, 
            EndGameView endGameView,
            ItemController itemController
            )
        {
            _sliderController = sliderController;
            _scoreControl = scoreControl;
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
            _scoreControl.Scored += End;
        }

        private void RestartGame()
        {
            Initialize();
        }

        private void End()
        {
            _itemController.EndControl();
            _sliderController.EndControl();
            _scoreControl.Scored -= End;
            _endGameView.Activate(RestartGame);
        }

        public void Dispose()
        {
            _scoreControl.Scored -= End;
        }
    }
}
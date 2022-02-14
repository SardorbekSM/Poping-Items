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
        private readonly SpawnController _spawnController;
        private readonly SliderController _sliderController;
        private readonly IScoreControl _scoreControl;
        private readonly EndGameView _endGameView;

        public GameProcess(SpawnController spawnController, SliderController sliderController, IScoreControl scoreControl, EndGameView endGameView)
        {
            _spawnController = spawnController;
            _sliderController = sliderController;
            _scoreControl = scoreControl;
            _endGameView = endGameView;
        }

        public void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _spawnController.StartControl();
            _sliderController.StartControl();
            _scoreControl.Scored += End;
        }

        private void RestartGame()
        {
            Initialize();
        }

        private void End()
        {
            _spawnController.EndControl();
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
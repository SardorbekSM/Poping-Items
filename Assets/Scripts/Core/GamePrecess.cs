using System;
using Control;
using Control.Interfaces;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Core
{
    public class GamePrecess : IStartable, IDisposable
    {
        private readonly SpawnControl _spawnControl;
        private readonly SliderControl _sliderControl;
        private readonly IScoreControl _scoreControl;
        private readonly EndGameView _endGameView;

        public GamePrecess(SpawnControl spawnControl, SliderControl sliderControl, IScoreControl scoreControl, EndGameView endGameView)
        {
            _spawnControl = spawnControl;
            _sliderControl = sliderControl;
            _scoreControl = scoreControl;
            _endGameView = endGameView;
        }

        public void Start()
        {
            _spawnControl.StartControl();
            _sliderControl.StartControl();
            _scoreControl.Scored += End;
        }

        private void RestartGame()
        {
            Debug.Log("Restart is working");
            
            Start();
        }

        private void End()
        {
            Debug.Log("End is working");

            _spawnControl.EndControl();
            _sliderControl.EndControl();
            _scoreControl.Scored -= End;
            _endGameView.Activate(RestartGame);

        }

        public void Dispose()
        {
            End();
        }
    }
}
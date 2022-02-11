using System;
using Control.Interfaces;
using Model;
using UnityEngine;
using View;

namespace Control
{
    public class ScoreControl : IScoreControl
    {
        private readonly GameProcessControl _spawnControl;
        private readonly EndGameView _endGameView;
        private readonly SliderModel _sliderModel;

        private float _score;

        public ScoreControl(GameProcessControl spawnControl, EndGameView endGameView, SliderModel sliderModel)
        {
            _spawnControl = spawnControl;
            _endGameView = endGameView;
            _sliderModel = sliderModel;
        }

        public float AddScore()
        {
            return _score += _sliderModel.Step;
        }

        public void OnScoreChanged(float score)
        {
            if (!(score >= _sliderModel.FillMax)) return;
            _spawnControl.StopSpawn();
            _endGameView.Activate(RestartGame);
            _spawnControl.OnWin();
        }

        private void RestartGame()
        {
            Debug.Log("Restart is working");
            
            _score = _sliderModel.FillMin;
            _spawnControl.BeginSpawn();
        }
    }
}

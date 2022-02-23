using System;

using Control.Interfaces;
using Model;

namespace Control
{
    public class LevelController : ILevelController
    {
        private readonly LevelModel _levelModel;
        private readonly GameController _gameController;

        private int _score;
        private int _iterationScore;
        
        public event Action Scored;

        public LevelController(LevelModel levelModel, GameController gameController)
        {
            _levelModel = levelModel;
            _gameController = gameController;
        }

        public void StartControl()
        {
            _gameController.Initialize();
        }

        public float AddScore()
        {
            var newScore = ++_score;
            
            OnScoreChanged();
            
            return newScore;
        }

        private void OnScoreChanged()
        {
            _iterationScore++;

            if (_iterationScore < _levelModel.IterationItemsCount) return;

            _iterationScore = _levelModel.StartValue;

            _gameController.Initialize();

            if (_score < _levelModel.LevelItemsCount) return;
            
            AllLevelsComplete();
        }

        private void AllLevelsComplete()
        {
            _score = _levelModel.StartValue;
            Scored?.Invoke();
        }

        public void EndControl()
        {
            _gameController.ResetToDefault();
        }
    }
}

using System;

using Core;
using Core.Spawner.Interfaces;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using View;

namespace Control
{
    public class LevelController : IStartable, IDisposable
    {
        private readonly LevelModel _levelModel;
        private readonly ISpawnerBehaviour _spawnerBehaviour;
        private readonly EndGameView _endGameView;

        private int _iterationScore;
        
        public LevelController(LevelModel levelModel, ISpawnerBehaviour spawnerBehaviour, EndGameView endGameView)
        {
            _spawnerBehaviour = spawnerBehaviour;
            _levelModel = levelModel;
            _endGameView = endGameView;
            levelModel.restarted += Start;
            levelModel.levelCompleted += Dispose;
        }
        
        public void Start()
        {
            _spawnerBehaviour.OnInstantiatedObject += SubscribeToClick;
        }

        private void SubscribeToClick(GameObject obj)
        {
            var item = obj.GetComponent<IClickBehaviour>();
            
            Assert.IsNotNull(item);

            item.ButtonClicked += () => TryAddScore(obj);
        }
        
        private void TryAddScore(GameObject obj)
        {
            var view = obj.GetComponent<ItemView>();
            
            Assert.IsNotNull(view); 

            if (view.PatternType != InteractableType.Correct) return;

            _levelModel.IncrementScore();
            OnScoreChanged();
            
            Debug.Log("Add Score");
        }

        private void OnScoreChanged()
        {
            if (_levelModel.IterationScore < _levelModel.IterationItemsCount) return;

           _levelModel.ResetIterationScore();

            if (_levelModel.LevelScore < _levelModel.LevelItemsCount) return;
            
            _levelModel.ResetLevelScore();
            
            AllLevelsComplete();
        }

        private void AllLevelsComplete()
        {
            _endGameView.Activate(_levelModel.restarted);
            _levelModel.levelCompleted?.Invoke();
        }

        public void Dispose()
        {
            _spawnerBehaviour.OnInstantiatedObject -= SubscribeToClick;
        }
    }
}

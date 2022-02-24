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
        private readonly ISpawnerBehaviour _spawner;
        private readonly EndGameView _endGameView;

        public LevelController(LevelModel levelModel, ISpawnerBehaviour spawner, EndGameView endGameView)
        {
            _spawner = spawner;
            _levelModel = levelModel;
            _endGameView = endGameView;
            levelModel.restarted += Start;
            levelModel.levelCompleted += Dispose;
        }
        
        public void Start()
        {
            _spawner.OnInstantiatedObject += SubscribeToClick;
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
        }

        public void Dispose()
        {
            _spawner.OnInstantiatedObject -= SubscribeToClick;
        }
    }
}

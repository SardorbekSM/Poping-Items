using System.Collections.Generic;
using Core.Randomizer;
using Data;
using Model;
using UnityEngine;

namespace Control
{
    public class GameController
    {
        private readonly IterationModel _iterationModel;
        private readonly IRandomizer _randomizer;
        private readonly IFactoryGameObject<GameObject> _factory;

        private List<GameObject> _correctPatterns;
        private List<GameObject> _wrongPatterns;

        private int _currentStep = 0;

        private GameObject CorrectPattern { get; set; }

        public GameController(IRandomizer randomizer, IterationModel iterationModel, IFactoryGameObject<GameObject> factory)
        {
            _iterationModel = iterationModel;
            _randomizer = randomizer;
            _factory = factory;
        }

        public void Initialize()
        {
            _iterationModel.InitializeNewPatterns();
            
            _correctPatterns = new List<GameObject>(_iterationModel.CorrectPatterns);
            _wrongPatterns = new List<GameObject>(_iterationModel.WrongPatterns);

            if (CorrectPattern == null)
            {
                _randomizer.SetMax(_correctPatterns.Count);
                var index = _randomizer.GetIndex();
            
                CorrectPattern = _correctPatterns[index];
            
                _correctPatterns.RemoveAt(index);
            }
            else
            {
                _correctPatterns.Remove(CorrectPattern);
            }

            if (!_iterationModel.UseCorrectsAsWrong) return;
            
            foreach (var pattern in _correctPatterns)
            {
                _wrongPatterns.Add(pattern);
            }
        }

        public GameObject GetPattern(out InteractableType type)
        {
            if (_currentStep >= _iterationModel.Frequency)
            {
                _currentStep = 0;
                type = InteractableType.Correct;
                return _factory.Create(CorrectPattern);
            }

            _currentStep++;
            _randomizer.SetMax(_wrongPatterns.Count);
            var index = _randomizer.GetIndex();
            type = InteractableType.Wrong;

            return _factory.Create(_wrongPatterns[index]);
        }

        public void ResetToDefault()
        {
            _correctPatterns.Clear();
            _wrongPatterns.Clear();
            CorrectPattern = null;
            _iterationModel.ResetToDefault();
        }
    }
}
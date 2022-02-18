using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Model
{
    public class PatternModel
    {
        private readonly LevelModel _levelModel;
        private readonly IRandomizer _randomizer;
        private readonly IFactoryGameObject<GameObject> _factory;

        private List<GameObject> _correctPatterns;
        private List<GameObject> _wrongPatterns;

        private int _currentStep = 0;

        private GameObject CorrectPattern { get; set; }

        public PatternModel(IRandomizer randomizer, LevelModel levelModel, IFactoryGameObject<GameObject> factory)
        {
            _levelModel = levelModel;
            _randomizer = randomizer;
            _factory = factory;
        }

        public void Initialize()
        {
            _correctPatterns = new List<GameObject>(_levelModel.CorrectPatterns);
            _wrongPatterns = new List<GameObject>(_levelModel.WrongPatterns);

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

            if (!_levelModel.UseCorrectsAsWrong) return;
            
            foreach (var pattern in _correctPatterns)
            {
                _wrongPatterns.Add(pattern);
            }
        }

        public GameObject GetPattern(out InteractableType type)
        {
            if (_currentStep >= _levelModel.Frequency)
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
        }
        
    }
}
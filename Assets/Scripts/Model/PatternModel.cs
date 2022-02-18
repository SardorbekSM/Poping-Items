using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Model
{
    public class PatternModel
    {
        private readonly LevelPatternsModel _levelPatternsModel;
        private readonly IRandomizer _randomizer;
        private readonly IFactoryGameObject<GameObject> _factory;

        private List<GameObject> _correctPatterns;
        private List<GameObject> _wrongPatterns;

        private int _currentStep = 0;

        private GameObject CorrectPattern { get; set; }

        public PatternModel(IRandomizer randomizer, LevelPatternsModel levelPatternsModel, IFactoryGameObject<GameObject> factory)
        {
            _levelPatternsModel = levelPatternsModel;
            _randomizer = randomizer;
            _factory = factory;
        }

        public void Initialize()
        {
            _levelPatternsModel.InitializeNewPatterns();
            
            _correctPatterns = new List<GameObject>(_levelPatternsModel.CorrectPatterns);
            _wrongPatterns = new List<GameObject>(_levelPatternsModel.WrongPatterns);

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

            if (!_levelPatternsModel.UseCorrectsAsWrong) return;
            
            foreach (var pattern in _correctPatterns)
            {
                _wrongPatterns.Add(pattern);
            }
        }

        public GameObject GetPattern(out InteractableType type)
        {
            if (_currentStep >= _levelPatternsModel.Frequency)
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
            _levelPatternsModel.ResetToDefault();
        }
        
    }
}
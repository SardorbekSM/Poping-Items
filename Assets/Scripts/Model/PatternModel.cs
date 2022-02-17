using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Model
{
    public class PatternModel
    {
        private readonly PatternsData _patterns;
        private readonly IRandomizer _randomizer;
        private readonly IFactoryGameObject<GameObject> _factory;

        private List<GameObject> _correctPatterns;
        private List<GameObject> _wrongPatterns;
        private GameObject _correctPattern;

        private int _currentStep = 0;
        
        public GameObject CorrectPattern => _correctPattern;

        public PatternModel(PatternsData patterns, IRandomizer randomizer)
        {
            _patterns = patterns;
            _randomizer = randomizer;
            _factory = new GameObjectFactory();
            Initialize();
        }

        public void Initialize()
        {
            _correctPatterns = new List<GameObject>(_patterns.CorrectPatterns);
            _wrongPatterns = new List<GameObject>(_patterns.WrongPatterns);

            _randomizer.SetMax(_correctPatterns.Count);
            var index = _randomizer.GetIndex();
            
            _correctPattern = _correctPatterns[index];
            
            _correctPatterns.RemoveAt(index);
            
            foreach (var pattern in _correctPatterns)
            {
                _wrongPatterns.Add(pattern);
            }
        }

        public GameObject GetPattern(out InteractableType type)
        {
            if (_currentStep >= _patterns.Frequency)
            {
                _currentStep = 0;
                type = InteractableType.Correct;
                return _factory.Create(_correctPattern);
            }

            _currentStep++;
            _randomizer.SetMax(_wrongPatterns.Count);
            var index = _randomizer.GetIndex();
            type = InteractableType.Wrong;

            return _factory.Create(_wrongPatterns[index]);
        }
    }
}
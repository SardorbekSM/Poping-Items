using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Model
{
    public class ItemModel
    {
        private readonly ItemPatternsData _itemPatterns;
        private readonly IRandomizer _randomizer;
        private List<GameObject> _correctPatterns;
        private List<GameObject> _wrongPatterns;

        private Dictionary<InteractableType, GameObject> _correctGameObjects;
        private Dictionary<InteractableType, GameObject> _wrongGameObjects;

        private GameObject _correctPattern;
        
        public GameObject CorrectPattern => _correctPattern;
        public List<GameObject> WrongPatterns => _wrongPatterns;
        public List<GameObject> CorrectPatterns => _correctPatterns;

        public ItemModel(ItemPatternsData itemPatterns, IRandomizer randomizer)
        {
            _itemPatterns = itemPatterns;
            _randomizer = randomizer;
            Initialize();
        }

        public void Initialize()
        {
            _correctPatterns = new List<GameObject>(_itemPatterns.CorrectPatterns);
            _wrongPatterns = new List<GameObject>(_itemPatterns.WrongPatterns);

            _randomizer.SetMax(_correctPatterns.Count);
            var index = _randomizer.GetIndex();
            
            _correctPattern = _correctPatterns[index];
            
            _correctPatterns.RemoveAt(index);
            
            foreach (var pattern in _correctPatterns)
            {
                _wrongPatterns.Add(pattern);
            }
        }

        public GameObject GetPattern()
        {
            _randomizer.SetMax(_wrongPatterns.Count);
            var index = _randomizer.GetIndex();

            return Object.Instantiate(_wrongPatterns[index]);
        }
    }
}
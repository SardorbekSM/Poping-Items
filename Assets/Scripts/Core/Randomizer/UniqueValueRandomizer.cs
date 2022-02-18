using System.Collections.Generic;
using UnityEngine;

namespace Core.Randomizer
{
    public class UniqueValueRandomizer : IRandomizer
    {
        private readonly List<int> _objectsIndex = new List<int>();
        
        private int _currentId = 0;
        private int _max;
        
        public void SetMax(int max)
        {
            _max = max;
            _objectsIndex.Clear();
            
            for (var i = 0; i < _max; i++)
            {
                if (i == _currentId && _max > 1)
                {
                    continue;
                }
                
                _objectsIndex.Add(i);
            }
        }

        public int GetIndex()
        {
            var selectId = _objectsIndex[Random.Range(0, _objectsIndex.Count)];
            
            if (_currentId <= _max)
            {
                _objectsIndex.Add(_currentId);
            }

            _objectsIndex.Remove(selectId);
            _currentId = selectId;

            return _currentId;
        }
    }
}

using System.Collections.Generic;

using UnityEngine;

using Data;


namespace Model
{
    public class IterationModel
    {
        private readonly IterationPatternsData _iterationPatternsData;
        private int _currentIteration = 0;
        private readonly int _iterationsCount = 0;

        public List<GameObject> CorrectPatterns { get; private set; }
        public List<GameObject> WrongPatterns { get; private set; }
        public float Frequency { get; private set; }
        public bool UseCorrectsAsWrong { get; private set; }

        public IterationModel(IterationPatternsData iterationPatternsData)
        {
            _iterationPatternsData = iterationPatternsData;
            
            _iterationsCount = iterationPatternsData.IterationPatterns.Length;
        }

        public void UpdatePatterns()
        {
            if (_currentIteration < _iterationsCount)
            {
                CorrectPatterns = new List<GameObject>(_iterationPatternsData.IterationPatterns[_currentIteration].CorrectPatterns);
                WrongPatterns = new List<GameObject>(_iterationPatternsData.IterationPatterns[_currentIteration].WrongPatterns);
                Frequency = _iterationPatternsData.IterationPatterns[_currentIteration].Frequency;
                UseCorrectsAsWrong = _iterationPatternsData.IterationPatterns[_currentIteration].UseCorrectsAsWrong;
                
                _currentIteration++;
                
                return;
            }
            
            ResetToDefault();
        }

        public void ResetToDefault()
        {
            _currentIteration = 0;
            CorrectPatterns.Clear();
            WrongPatterns.Clear();
        }
    }
}
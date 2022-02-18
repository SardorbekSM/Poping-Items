using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class PatternsData : ScriptableObject
    {
        [Tooltip("The Frequency of choosing the correct pattern")]
        [SerializeField] private int _frequency;

        [SerializeField] private bool _useCorrectsAsWrong;
        
        [SerializeField] private SpawnData _correctPatterns;
        [SerializeField] private SpawnData _wrongPatterns;

        public int Frequency => _frequency;
        public IEnumerable<GameObject> CorrectPatterns => _correctPatterns.Prefabs;
        public IEnumerable<GameObject> WrongPatterns => _wrongPatterns.Prefabs;
        public bool UseCorrectsAsWrong => _useCorrectsAsWrong;
    }
}
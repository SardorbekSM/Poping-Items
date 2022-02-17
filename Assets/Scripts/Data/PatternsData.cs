using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class PatternsData : ScriptableObject
    {
        [Tooltip("The Frequency of choosing the correct pattern")]
        [SerializeField] private int _frequency;
        
        [SerializeField] private GameObject[] _correctPatterns;
        [SerializeField] private GameObject[] _wrongPatterns;

        [SerializeField] public PatternData[] _patternsData;

        public int Frequency => _frequency;

        public IEnumerable<GameObject> CorrectPatterns => _correctPatterns;
        public IEnumerable<GameObject> WrongPatterns => _wrongPatterns;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class ItemPatternsData : ScriptableObject
    {
        [SerializeField] private GameObject[] _correctPatterns;
        [SerializeField] private GameObject[] _wrongPatterns;

        public IEnumerable<GameObject> CorrectPatterns => _correctPatterns;
        public IEnumerable<GameObject> WrongPatterns => _wrongPatterns;
    }
}
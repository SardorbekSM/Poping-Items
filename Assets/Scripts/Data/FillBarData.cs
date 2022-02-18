using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "FillBarData", menuName = "FillBarData", order = 0)]
    public class FillBarData : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private LevelPatternsData _levelPatternsData;
        [SerializeReference] private int _iterationsCount;
#endif
        
        [SerializeField, Min(0)] private int _fillMin = 0;
        [SerializeReference] private int _fillMax;
        [SerializeField] private int _step;

        [SerializeField] private int _requiredItemsCount;

        public int FillMin => _fillMin;
        public int FillMax => _fillMax;
        public int Step => _step;
        public int RequiredItemsCount => _requiredItemsCount;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _iterationsCount = _levelPatternsData.LevelPatterns.Length;
            _fillMax = RequiredItemsCount * _iterationsCount;

            if ((_fillMax % _iterationsCount) == 0) return;
            
            Debug.LogError("_fillMax");
        }
#endif
    }
}
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private IterationPatternsData _iterationPatternsData;
        [SerializeReference] private int _iterationsCount;
#endif
        
        [SerializeField, Min(0)] private int _startValue = 0;
        [SerializeReference] private int _levelRequiredItemsCount;

        [SerializeField] private int _iterationRequiredItemsCount;

        public int StartValue => _startValue;
        public int LevelRequiredItemsCount => _levelRequiredItemsCount;
        public int IterationRequiredItemsCount => _iterationRequiredItemsCount;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _iterationsCount = _iterationPatternsData.IterationPatterns.Length;
            _levelRequiredItemsCount = IterationRequiredItemsCount * _iterationsCount;

            if ((_levelRequiredItemsCount % _iterationsCount) == 0) return;
            
            Debug.LogError("_levelRequiredItemsCount");
        }
#endif
    }
}
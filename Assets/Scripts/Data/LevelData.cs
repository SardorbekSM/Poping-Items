using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private LevelPatternsData _levelPatternsData;
        [SerializeReference] private int _iterationsCount;
#endif
        
        [SerializeField, Min(0)] private int _startValue = 0;
        [SerializeReference] private int _levelRequiredItemsCount;
        [SerializeField] private int _step;

        [SerializeField] private int _iterationItemsCount;

        public int StartValue => _startValue;
        public int LevelRequiredItemsCount => _levelRequiredItemsCount;
        public int Step => _step;
        public int IterationItemsCount => _iterationItemsCount;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _iterationsCount = _levelPatternsData.LevelPatterns.Length;
            _levelRequiredItemsCount = IterationItemsCount * _iterationsCount;

            if ((_levelRequiredItemsCount % _iterationsCount) == 0) return;
            
            Debug.LogError("_levelRequiredItemsCount");
        }
#endif
    }
}
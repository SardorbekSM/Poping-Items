using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class LevelPatternsData : ScriptableObject
    {
        [SerializeField] private PatternsData[] _levelPatternsData;

        public PatternsData[] LevelPatterns => _levelPatternsData;
    }
}
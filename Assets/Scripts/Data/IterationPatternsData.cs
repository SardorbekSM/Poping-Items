using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class IterationPatternsData : ScriptableObject
    {
        [SerializeField] private PatternsData[] _iterationPatternsData;

        public PatternsData[] IterationPatterns => _iterationPatternsData;
    }
}
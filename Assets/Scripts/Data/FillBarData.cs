using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "FillBarData", menuName = "FillBarData", order = 0)]
    public class FillBarData : ScriptableObject
    {
        [SerializeField, Min(0)] private float _fillMin = 0;
        [SerializeField] private float _fillMax;
        [SerializeField] private float _step;

        public float FillMin => _fillMin;
        public float FillMax => _fillMax;
        public float Step => _step;
    }
}
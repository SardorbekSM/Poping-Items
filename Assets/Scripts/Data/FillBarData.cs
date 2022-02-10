using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "FillBarData", menuName = "FillBarData", order = 0)]
    public class FillBarData : ScriptableObject
    {
        [SerializeField] private float _fillMax;
        [SerializeField] private float _step;

        public float FillMax => _fillMax;
        public float Step => _step;
    }
}
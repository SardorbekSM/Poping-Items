using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class ItemsOffsetData : ScriptableObject
    {
        [SerializeField] private float _leftOffset;
        [SerializeField] private float _rightOffset;
        [SerializeField] private float _bottomOffset;

        public float LeftOffset => _leftOffset;
        public float RightOffset => _rightOffset;
        public float BottomOffset => _bottomOffset;
    }
}
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class ColorsData : ScriptableObject
    {
        [SerializeField] private Color[] _colors;
    }
}

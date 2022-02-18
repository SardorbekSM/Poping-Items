using UnityEngine;

namespace Core.Position
{
    public interface IPositionGetter
    {
        Vector2 GetDefault();
        Vector2 GetRandom();
    }
}
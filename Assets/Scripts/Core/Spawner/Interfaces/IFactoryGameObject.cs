using UnityEngine;

public interface IFactoryGameObject<T> : IFactory<T, Object> where T : Object
{
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFactory : IFactoryGameObject<GameObject>
{
    public GameObject Create(Object prefab)
    {
        return (GameObject) Object.Instantiate(prefab);
    }
}

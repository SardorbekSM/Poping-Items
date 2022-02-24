using UnityEngine;

namespace Core.Spawner
{
    public class GameObjectFactory : IFactoryGameObject<GameObject>
    {
        public GameObject Create(Object prefab)
        {
            return (GameObject) Object.Instantiate(prefab);
        }
    }
}

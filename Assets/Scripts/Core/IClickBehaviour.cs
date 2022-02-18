using System;
using UnityEngine;

namespace Core
{
    public interface IClickBehaviour
    {
        event Action<GameObject> ButtonClicked;
        void OnButtonClicked();
    }
}
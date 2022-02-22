using System;
using UnityEngine;

namespace Core
{
    public interface IClickBehaviour
    {
        event Action ButtonClicked;
        void OnButtonClicked();
    }
}
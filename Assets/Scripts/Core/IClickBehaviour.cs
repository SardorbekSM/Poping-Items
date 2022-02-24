using System;

namespace Core
{
    public interface IClickBehaviour
    {
        event Action ButtonClicked;
    }
}
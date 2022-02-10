using System;

public interface IClickBehaviour
{
    event Action ButtonClicked;
    void OnButtonClicked();
}
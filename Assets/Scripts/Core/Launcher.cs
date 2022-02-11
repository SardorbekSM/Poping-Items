using Control;
using VContainer.Unity;

namespace Core
{
    public class Launcher : IStartable
    {
        private readonly GameProcessControl _gameProcessControl;

        public Launcher(GameProcessControl gameProcessControl)
        {
            _gameProcessControl = gameProcessControl;
        }

        public void Start()
        {
            _gameProcessControl.BeginSpawn();
        }
    }
}
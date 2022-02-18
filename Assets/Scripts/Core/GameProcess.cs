using System;
using Control.Interfaces;
using VContainer.Unity;
using View;

namespace Core
{
    public class GameProcess : IStartable, IDisposable
    {
        private readonly ISliderController _sliderController;
        private readonly ILevelController _levelController;
        private readonly IItemController _itemController;
        private readonly EndGameView _endGameView;

        public GameProcess(ISliderController sliderController, 
            ILevelController levelController, IItemController itemController, 
            EndGameView endGameView)
        {
            _sliderController = sliderController;
            _levelController = levelController;
            _itemController = itemController;
            _endGameView = endGameView;
        }

        public void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _itemController.StartControl();
            _sliderController.StartControl();
            _levelController.StartControl();
            _levelController.Scored += End;
        }

        private void RestartGame()
        {
            Initialize();
        }

        private void End()
        {
            _endGameView.Activate(RestartGame);
            Dispose();
        }

        public void Dispose()
        {
            _itemController.EndControl();
            _sliderController.EndControl();
            _levelController.EndControl();
            _levelController.Scored -= End;
        }
    }
}
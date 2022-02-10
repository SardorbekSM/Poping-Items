using UnityEngine;
using View;

namespace Control
{
    public class ScoreControl
    {
        private readonly GameProcessControl _spawnControl;
        private readonly EndGameView _endGameView;

        public ScoreControl(GameProcessControl spawnControl, EndGameView endGameView)
        {
            _spawnControl = spawnControl;
            _endGameView = endGameView;
        }

        public void OnScoreChanged(float sliderValue)
        {
            if (!(sliderValue >= 10)) return;
            _spawnControl.StopLooping();
            _endGameView.Activate(RestartGame);
        }

        private void RestartGame()
        {
            Debug.Log("Restart is working");
            _spawnControl.Start();
        }
    }
}

using View;

namespace Control
{
    public class ScoreControl
    {
        private readonly LoopedSpawnControl _spawnControl;
        private readonly EndGameView _endGameView;

        public ScoreControl(LoopedSpawnControl spawnControl, EndGameView endGameView)
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
            _spawnControl.Start();
        }
    }
}

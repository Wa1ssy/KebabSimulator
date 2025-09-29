using System;

namespace KebabSimulator.ApplicationServices
{
    public interface IGameService
    {
        event EventHandler<int> ScoreChanged;
        void Start();
        void Pause();
        void AddScore(int delta);
    }

    public class GameService : IGameService
    {
        private int _score;
        public event EventHandler<int> ScoreChanged;

        public void Start()
        {
            _score = 0;
            OnScoreChanged();
        }

        public void Pause()
        {
            // siia saad panna loogika mängu pausile panemiseks
        }

        public void AddScore(int delta)
        {
            _score += delta;
            OnScoreChanged();
        }

        private void OnScoreChanged()
        {
            ScoreChanged?.Invoke(this, _score);
        }
    }
}

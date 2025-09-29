using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using KebabSimulator.ApplicationServices;

namespace KebabSimulator.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly IGameService _gameService;
        private int _score;
        private bool _isRunning;

        public event PropertyChangedEventHandler PropertyChanged;

        public GameViewModel(IGameService gameService)
        {
            _gameService = gameService;

            StartCommand = new RelayCommand(StartGame, () => !IsRunning);
            PauseCommand = new RelayCommand(PauseGame, () => IsRunning);

            _gameService.ScoreChanged += OnScoreChanged;
        }

        public ICommand StartCommand { get; }
        public ICommand PauseCommand { get; }

        public int Score
        {
            get => _score;
            private set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            private set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged();
                    ((RelayCommand)StartCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)PauseCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private void StartGame()
        {
            _gameService.Start();
            IsRunning = true;
        }

        private void PauseGame()
        {
            _gameService.Pause();
            IsRunning = false;
        }

        private void OnScoreChanged(object sender, int newScore)
        {
            Score = newScore;
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

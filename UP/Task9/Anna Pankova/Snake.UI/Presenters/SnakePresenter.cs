using Snake.UI.Interfaces;
using Snake.UI.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Snake.UI.Presenters
{
    class SnakePresenter
    {
        private readonly ISnakeGameView _view;
        private readonly INavigationService _navigationService;
        private readonly IGameModel _model;
        private readonly IScoresService _scoreService;
        private Thread thread;
        private readonly Dispatcher _mainDispatcher;

        public SnakePresenter(ISnakeGameView view, INavigationService navigationService, IGameModel model, IScoresService scoresService)
        {
            _view = view;
            _navigationService = navigationService;
            _model = model;
            _scoreService = scoresService;

            _mainDispatcher = Dispatcher.CurrentDispatcher;

            _view.PaintRequested += OnViewPaintRequested;
            _view.KeyDownEvent += OnKeyDownPressed;
            _model.GameOver += _model_GameOver;
            _model.ScoreUpdated += OnScoreUpdated;

            _navigationService.Navigation += _navigationService_Navigation;
        }

        private void OnScoreUpdated(object sender, EventArgs e)
        {
            _view.YourScore = _model.YourScore;
        }

        private void _navigationService_Navigation(object sender, string e)
        {
            if (e == ApplicationViews.SnakeGame)
            {
                _view.YourScore = 0;
                _model.ResetSnake();
                _view.BestScore = _scoreService.BestScore;
                StartTimer();
            }
        }

        private void _model_GameOver(object sender, EventArgs e)
        {
            _scoreService.AddScore(_model.GameName, _model.YourScore);
            _view.BestScore = _scoreService.BestScore;
            MessageBox.Show("GAME OVER", "Snake", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            _navigationService.NavigateTo(ApplicationViews.Scores);

            thread.Abort();
        }

        void StartTimer()
        {
            if (thread == null || !thread.IsAlive)
            {
                thread = new Thread(BackgroundTimer);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        void OnViewPaintRequested(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            _view.DrawFood(g, _model.FoodCoordinates);
            _view.DrawSnake(g, _model.SnakeCoordinates);
        }
        void OnKeyDownPressed(object sender, KeyEventArgs e)
        {
            int dir = _model.Direction;
            if (e.KeyCode == Keys.Right && dir != 3)
            {
                dir = 1;
            }
            else
            if (e.KeyCode == Keys.Down && dir != 4)
            {
                dir = 2;
            }
            else
            if (e.KeyCode == Keys.Left && dir != 1)
            {
                dir = 3;
            }
            else
            if (e.KeyCode == Keys.Up && dir != 2)
            {
                dir = 4;
            }
            _model.Direction = dir;
        }
        void BackgroundTimer()
        {
            while (true)
            {
                _mainDispatcher.Invoke(new Action(() =>
                {
                    _model.Step();
                }));
                _view.InvalidateArea();
                Thread.Sleep(100 - _model.YourScore * 5);
            }
        }
    }
}

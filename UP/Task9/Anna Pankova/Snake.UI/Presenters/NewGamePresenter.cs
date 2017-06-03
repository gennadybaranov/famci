using System;
using Snake.UI.Interfaces;
using Snake.UI.Navigation;
using System.IO;

namespace Snake.UI.Presenters
{
    public class NewGamePresenter
    {
        private readonly INavigationService _navigationService;
        private readonly INewGameView _view;
        private readonly IGameModel _model;
        public NewGamePresenter(
            INewGameView view,
            INavigationService navigationService, IGameModel model)
        {
            _view = view;
            _navigationService = navigationService;
            _model = model;
            _view.GameName = _model.GameName;

            _view.NewGameStartRequested += OnViewNewGameStartRequested;
            _view.ScoresRequested += OnViewScoresRequested;
        }

        private void OnViewScoresRequested(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ApplicationViews.Scores);
        }

        private void OnViewNewGameStartRequested(object sender, EventArgs e)
        {
            _model.GameName = _view.GameName;
            _navigationService.NavigateTo(ApplicationViews.SnakeGame);
        }
    }
}

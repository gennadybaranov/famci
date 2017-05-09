using System;
using Snake.UI.Interfaces;
using Snake.UI.Navigation;

namespace Snake.UI.Presenters
{
    public class NewGamePresenter
    {
        private readonly INavigationService _navigationService;
        private readonly INewGameView _view;

        public NewGamePresenter(
            INewGameView view,
            INavigationService navigationService)
        {
            _view = view;
            _navigationService = navigationService;

            _view.NewGameStartRequested += OnViewNewGameStartRequested;
        }


        private void OnViewNewGameStartRequested(object sender, EventArgs e)
        {
            //TODO: Save to file started game name: _view.GameName;

            _navigationService.NavigateTo(ApplicationViews.SnakeGame);
        }
    }
}

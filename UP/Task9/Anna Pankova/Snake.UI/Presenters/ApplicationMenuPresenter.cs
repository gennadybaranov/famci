using System;
using Snake.UI.Interfaces;
using Snake.UI.Navigation;

namespace Snake.UI.Presenters
{
    public class ApplicationMenuPresenter
    {
        private readonly INavigationService _navigationService;


        public ApplicationMenuPresenter(IApplicationMenu menu, INavigationService navigationService)
        {
            _navigationService = navigationService;

            menu.NewGameRequested += OnMenuNewGameRequested;
        }


        private void OnMenuNewGameRequested(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ApplicationViews.NewGame);
        }
    }
}

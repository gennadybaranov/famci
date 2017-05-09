using Snake.UI.Interfaces;
using Snake.UI.Navigation;
using Snake.UI.Presenters;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Snake.UI
{
    public partial class Form1 : Form, IApplicationMenu, INewGameView, ISnakeGameView //TODO: Add new IBestScoresView
    {
        private readonly NewGamePresenter _newGamePresenter;
        private readonly ApplicationMenuPresenter _applicationMenuPresenter;

        public string GameName
        {
            get
            {
                return gameNameTextBox.Text;
            }
        }


        public event EventHandler NewGameStartRequested;

        public event EventHandler NewGameRequested;


        public Form1(INavigationService navigationService)
        {
            InitializeComponent();

            navigationService.RegisterViews(
                new KeyValuePair<string, INavigationView>(
                    ApplicationViews.NewGame,
                    new NavigationView(() => createNewGamePanel.Visible = true, () => createNewGamePanel.Visible = false)),
                new KeyValuePair<string, INavigationView>(
                    ApplicationViews.SnakeGame,
                    new NavigationView(() => snakeGamePanel.Visible = true, () => snakeGamePanel.Visible = false))
            //TODO: Register new views here
            );

            _applicationMenuPresenter = new ApplicationMenuPresenter(this, navigationService);
            _newGamePresenter = new NewGamePresenter(this, navigationService);
            //TODO: add new presenters

            navigationService.NavigateTo(ApplicationViews.NewGame);
        }


        private void OnStartNewGameButtonClicked(object sender, EventArgs e)
        {
            NewGameStartRequested?.Invoke(this, e);
        }

        private void OnNewGameMenuItemClicked(object sender, EventArgs e)
        {
            NewGameRequested?.Invoke(this, e);
        }
    }
}

using Snake.UI.Interfaces;
using Snake.UI.Navigation;
using Snake.UI.Presenters;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace Snake.UI
{
    public partial class Form1 : Form, IApplicationMenu, INewGameView, ISnakeGameView, IBestScoresView
    {
        private readonly NewGamePresenter _newGamePresenter;
        private readonly ApplicationMenuPresenter _applicationMenuPresenter;
        private readonly ScoresPresenter _scoresPresenter;
        private readonly SnakePresenter _snakePresenter;

        public string GameName
        {
            get
            {
                return gameNameTextBox.Text;
            }
            set
            {
                gameNameTextBox.Text = value;
            }
        }

        List<Tuple<string, int>> IBestScoresView.Scores
        {
            set
            {
                listScores.Items.Clear();
                foreach (var item in value)
                {
                    string[] row = { item.Item1, item.Item2.ToString() };
                    var listViewItem = new ListViewItem(row);
                    listScores.Items.Add(listViewItem);
                }
            }
        }

        public int BestScore
        {
            set
            {
                bestScore.Text = value.ToString();
            }
        }

        public int YourScore
        {
            set
            {
                yourScore.Text = value.ToString();
            }
        }

        public event EventHandler NewGameStartRequested;

        public event EventHandler NewGameRequested;

        public event EventHandler ScoresRequested;

        public event EventHandler<PaintEventArgs> PaintRequested;

        public new event EventHandler<KeyEventArgs> KeyDownEvent;

        public Form1(INavigationService navigationService, IGameModel gameModel, IScoresService scoresService)
        {
            InitializeComponent();

            navigationService.RegisterViews(
                new KeyValuePair<string, INavigationView>(
                    ApplicationViews.NewGame,
                    new NavigationView(() => createNewGamePanel.Visible = true, () => createNewGamePanel.Visible = false)),
                new KeyValuePair<string, INavigationView>(
                    ApplicationViews.SnakeGame,
                    new NavigationView(() => snakeGamePanel.Visible = true, () => snakeGamePanel.Visible = false)),
                new KeyValuePair<string, INavigationView>(
                    ApplicationViews.Scores,
                    new NavigationView(() => scoresPanel.Visible = true, () => scoresPanel.Visible = false))
            );

            _applicationMenuPresenter = new ApplicationMenuPresenter(this, navigationService);
            _newGamePresenter = new NewGamePresenter(this, navigationService, gameModel);
            _snakePresenter = new SnakePresenter(this, navigationService, gameModel, scoresService);
            _scoresPresenter = new ScoresPresenter(this, navigationService, scoresService);

            navigationService.NavigateTo(ApplicationViews.NewGame);

            //Double buffering hack
            Type controlType = snakeGamePanel.GetType();
            PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(snakeGamePanel, true, null);
        }


        private void OnStartNewGameButtonClicked(object sender, EventArgs e)
        {
            NewGameStartRequested?.Invoke(this, e);
        }

        private void OnNewGameMenuItemClicked(object sender, EventArgs e)
        {
            NewGameRequested?.Invoke(this, e);
        }

        private void scoresMenuItem_Click(object sender, EventArgs e)
        {
            ScoresRequested?.Invoke(this, e);
        }

        private void scoresButton_Click(object sender, EventArgs e)
        {
            ScoresRequested?.Invoke(this, e);
        }
        public void DrawSnake(Graphics g, List<Point> snakeCoordinates)
        {
            int cellSize = GetCellSize();
            SolidBrush bodyBrush = new SolidBrush(Color.GreenYellow);
            SolidBrush brush = new SolidBrush(Color.LawnGreen);
            foreach (var coordinate in snakeCoordinates)
            {
                Rectangle rect = new Rectangle(coordinate.X * cellSize, coordinate.Y * cellSize, cellSize - 1, cellSize - 1);
                g.FillRectangle(brush, rect);
                brush = bodyBrush;
            }
        }

        public void DrawFood(Graphics g, Point coordinates)
        {
            int cellSize = GetCellSize();
            SolidBrush brush = new SolidBrush(Color.Coral);
            Rectangle rect = new Rectangle(coordinates.X * cellSize, coordinates.Y * cellSize, cellSize - 1, cellSize - 1);
            g.FillRectangle(brush, rect);

        }
        private int GetCellSize()
        {
            int width = snakeGamePanel.Width;
            int height = snakeGamePanel.Height - statusStrip1.Height;

            return Math.Min(width / GameModel.WIDTH, height / GameModel.HEIGHT);
        }

        private void snakeGamePanel_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.LightGray);
            Rectangle rect = new Rectangle(-1, -1, GameModel.WIDTH * GetCellSize() + 1, GameModel.HEIGHT * GetCellSize() + 1);
            e.Graphics.DrawRectangle(pen, rect);
            PaintRequested?.Invoke(this, e);
        }

        public void InvalidateArea()
        {
            snakeGamePanel.Invalidate();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEvent?.Invoke(this, e);
        }
    }
}

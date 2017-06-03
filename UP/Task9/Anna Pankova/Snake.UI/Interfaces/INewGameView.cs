using System;

namespace Snake.UI.Interfaces
{
    public interface INewGameView
    {
        string GameName { get; set; }

        event EventHandler NewGameStartRequested;
        event EventHandler ScoresRequested;
    }
}

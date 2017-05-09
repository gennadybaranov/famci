using System;

namespace Snake.UI.Interfaces
{
    public interface INewGameView
    {
        string GameName { get; }


        event EventHandler NewGameStartRequested;
    }
}

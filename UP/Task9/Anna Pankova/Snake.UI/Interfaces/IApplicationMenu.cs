using System;

namespace Snake.UI.Interfaces
{
    public interface IApplicationMenu
    {
        event EventHandler NewGameRequested;
        event EventHandler ScoresRequested;
    }
}

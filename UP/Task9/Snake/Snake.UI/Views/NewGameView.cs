using System;
using Snake.UI.Interfaces;

namespace Snake.UI.Views
{
    public class NewGameView : INewGameView
    {
        public string GameName
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public event EventHandler NewGameStartRequested;
    }
}


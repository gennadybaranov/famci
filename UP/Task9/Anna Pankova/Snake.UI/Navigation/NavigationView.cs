using System;

namespace Snake.UI.Navigation
{
    public class NavigationView : INavigationView
    {
        private readonly Action _showAction;
        private readonly Action _hideAction;


        public NavigationView(Action show, Action hide)
        {
            _showAction = show;
            _hideAction = hide;
        }


        void INavigationView.Hide()
        {
            _hideAction();
        }

        void INavigationView.Show()
        {
            _showAction();
        }
    }
}

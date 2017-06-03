using Snake.UI.Interfaces;
using System.Collections.Generic;
using System;

namespace Snake.UI.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IDictionary<string, INavigationView> _navigationViews;

        private INavigationView _activeView;

        public event EventHandler<string> Navigation;

        public NavigationService()
        {
            _navigationViews = new Dictionary<string, INavigationView>();
        }


        public void RegisterViews(params KeyValuePair<string, INavigationView>[] views)
        {
            foreach (var view in views)
            {
                _navigationViews.Add(view.Key, view.Value);
            }
        }

        public bool NavigateTo(string view)
        {
            INavigationView navigateToView;
            Navigation?.Invoke(this, view);
            if (_navigationViews.TryGetValue(view, out navigateToView))
            {
                foreach (var item in _navigationViews.Values)
                {
                    item.Hide();
                }

                _activeView = navigateToView;
                _activeView.Show();

                return true;
            }

            return false;
        }
    }
}

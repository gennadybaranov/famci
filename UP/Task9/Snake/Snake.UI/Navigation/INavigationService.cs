﻿using Snake.UI.Interfaces;
using System.Collections.Generic;

namespace Snake.UI.Navigation
{
    public interface INavigationService
    {
        void RegisterViews(params KeyValuePair<string, INavigationView>[] views);

        bool NavigateTo(string view);
    }
}

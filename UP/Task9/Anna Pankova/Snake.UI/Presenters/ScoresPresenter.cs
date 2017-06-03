using Snake.UI.Interfaces;
using Snake.UI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake.UI.Presenters
{
    class ScoresPresenter
    {
        readonly private IBestScoresView _view;
        private readonly INavigationService _navigationService;
        private readonly IScoresService _model;

        public ScoresPresenter(IBestScoresView view, INavigationService navigationService,
            IScoresService model)
        {
            _view = view;
            _navigationService = navigationService;
            _model = model;

            _navigationService.Navigation += _navigationService_Navigation;
        }

        private void _navigationService_Navigation(object sender, string e)
        {
            if (e == ApplicationViews.Scores)
            {
                _view.Scores = _model.Scores;
            }
        }
    }
}

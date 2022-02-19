using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardGraphGame;

namespace WpfGraphGame.ViewModels
{
    public class WindowRoadCreateViewModel
    {
        #region fields

        GameGraph _game;
        GraphRoad _road;

        #endregion

        #region propertyes

        public City City1
        {
            get => _road.City1;
            set
            {
                if (value != City2)
                    _road.City1 = value;
            }
        }

        public City City2
        {
            get => _road.City2;
            set
            {
                if (value != City1)
                    _road.City2 = value;
            }
        }

        public List<City> Citys
        {
            get
            {
                return _game.GetCitys();
            }
        }

        public int CountDrive
        {
            get => _road.CountPermittedPasses;
            set => _road.CountPermittedPasses = value;
        }

        #endregion

        #region constructors

        public WindowRoadCreateViewModel(GameGraph game, GraphRoad road)
        {
            _game = game;
            _road = road;
        }

        #endregion
    }
}

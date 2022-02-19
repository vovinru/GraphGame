using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardGraphGame;

namespace WpfGraphGame.ViewModels
{
    public class WindowTransportCreateViewModel
    {
        GameGraph _game;
        Transport _transport;

        public List<City> Citys
        {
            get
            {
                return _game.GetCitys();
            }
        }

        public City SelectedCity
        {
            get
            {
                return _transport.City;
            }
            set
            {
                _transport.City = value;
            }
        }

        public WindowTransportCreateViewModel(GameGraph game, Transport transport)
        {
            _game = game;
            _transport = transport;
        }
    }
}

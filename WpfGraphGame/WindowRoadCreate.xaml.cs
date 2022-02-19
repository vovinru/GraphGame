using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfGraphGame.ViewModels;
using StandardGraphGame;

namespace WpfGraphGame
{
    /// <summary>
    /// Логика взаимодействия для WindowRoadCreate.xaml
    /// </summary>
    public partial class WindowRoadCreate : Window
    {
        WindowRoadCreateViewModel _viewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="road"> если null то новая дорога между 1м и 2м городом</param>
        public WindowRoadCreate(GameGraph game, GraphRoad road = null)
        {
            InitializeComponent();

            if(road == null)
            {
                road = new GraphRoad();
                road.City1 = game.GetCitys()[0];
                road.City2 = game.GetCitys()[1];
                game.AddRoad(road);
            }

            _viewModel = new WindowRoadCreateViewModel(game, road);

            DataContext = _viewModel;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

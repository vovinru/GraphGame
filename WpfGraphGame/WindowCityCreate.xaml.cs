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
    /// Логика взаимодействия для WindowCityCreate.xaml
    /// </summary>
    public partial class WindowCityCreate : Window
    {
        WindowCityCreateViewModel _viewModel;
        GameGraph _game;
        City _city;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="city">если null создаем новый город</param>
        public WindowCityCreate(GameGraph game, City city = null)
        {
            InitializeComponent();

            ///Создание нвоого города
            if(city==null)
            {
                city = new City();
                game.AddCity(city);
            }

            _viewModel = new WindowCityCreateViewModel(game, city);
            _game = game;
            _city = city;

            Update();
        }

        private void Update()
        {
            DataContext = null;
            DataContext = _viewModel;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonCreateCargos_Click(object sender, RoutedEventArgs e)
        {
            WindowCreateCargo window = new WindowCreateCargo(_game, _city, false);
            window.ShowDialog();

            Update();
        }

        private void ButtonCreateCargosGoal_Click(object sender, RoutedEventArgs e)
        {
            WindowCreateCargo window = new WindowCreateCargo(_game, _city, true);
            window.ShowDialog();

            Update();
        }
    }
}

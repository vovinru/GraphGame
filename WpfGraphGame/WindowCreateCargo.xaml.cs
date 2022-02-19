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
    /// Логика взаимодействия для WindowCreateCargo.xaml
    /// </summary>
    public partial class WindowCreateCargo : Window
    {
        City _city;
        bool _goal;
        WindowCreateCargoViewModel _viewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">игра</param>
        /// <param name="city">город</param>
        /// <param name="goal">если true, то цель редактируем, если false, то груз в городе</param>
        public WindowCreateCargo(GameGraph game, City city, bool goal)
        {
            InitializeComponent();

            _city = city;
            _goal = goal;

            _viewModel = new WindowCreateCargoViewModel(_city, _goal);

            DataContext = _viewModel;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if(_goal)
            {
                _city.RemoveAllGargoGoal();
                foreach(WindowCreateCargoViewModel_CargoCount c in _viewModel.CargoCountList)
                {
                    if (c.Count > 0)
                        _city.AddCargoGoal(c.Type, c.Count);
                }
            }
            else
            {
                _city.RemoveAllCargo();
                foreach (WindowCreateCargoViewModel_CargoCount c in _viewModel.CargoCountList)
                {
                    for (int i = 0; i < c.Count; i++)
                        _city.AddCargo(new Cargo(c.Type));
                }
            }

            this.Close();
        }
    }
}

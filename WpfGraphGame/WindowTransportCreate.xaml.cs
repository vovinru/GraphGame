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
    /// Логика взаимодействия для WindowTransportCreate.xaml
    /// </summary>
    public partial class WindowTransportCreate : Window
    {
        WindowTransportCreateViewModel _viewModel;

        public WindowTransportCreate(GameGraph game, Transport transport)
        {
            InitializeComponent();

            _viewModel = new WindowTransportCreateViewModel(game, transport);
            DataContext = _viewModel;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

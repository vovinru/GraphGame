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
    /// Логика взаимодействия для WindowChooseGameObject.xaml
    /// </summary>
    public partial class WindowChooseGameObject : Window
    {
        WindowChooseGameObjectViewModel _viewModel;

        public WindowChooseGameObject(List<GameObject> gameObjects)
        {
            InitializeComponent();

            _viewModel = new WindowChooseGameObjectViewModel(gameObjects);
            DataContext = _viewModel;

        }
        
        public GameObject GetSelectedGameObject()
        {
            return _viewModel.SelectedGameObject;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfEngineDraw;
using StandardGraphGame;
using StandardGraphGameJSON;
using System.IO;
using Newtonsoft.Json;

namespace WpfGraphGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        WpfEngineDraw.WpfEngineDraw _engine;
        GameGraph _game;

        public MainWindow()
        {
            InitializeComponent();

            ConstantBase.X_LENGTH_GAME = (int)this.Width;
            ConstantBase.Y_LENGTH_GAME = (int)this.Height;

            ConstantBase.SCALE_COEFICIENT = 
                Math.Min((float)ConstantBase.X_LENGTH_GAME / (float)ConstantBase.X_LENGTH_GAME_BASE,
                            (float)ConstantBase.Y_LENGTH_GAME / (float)ConstantBase.Y_LENGTH_GAME_BASE);

            _engine = new WpfEngineDraw.WpfEngineDraw(CanvasTest);
            _game = new GameGraph(_engine, LoadLevel);
            _game.ReDraw(_engine);
        }

        private void CanvasTest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _game.Click((float)e.GetPosition(null).X, (float)e.GetPosition(null).Y);
        }

        private void LoadLevel(int numberLevel)
        {
            _game.ClearLevel();
            _game.LoadLevel(numberLevel);
            _game.StartLevel();
            _game.ReDraw();

        }
    }
}

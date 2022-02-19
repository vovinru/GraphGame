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
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using StandardGraphGame;
using StandardGraphGameJSON;

namespace WpfGraphGame
{
    /// <summary>
    /// Логика взаимодействия для LevelConstructor.xaml
    /// </summary>
    public partial class LevelConstructor : Window
    {

        WpfEngineDraw.WpfEngineDraw _engine;
        GameGraph _game;


        public LevelConstructor()
        {
            InitializeComponent();

            _engine = new WpfEngineDraw.WpfEngineDraw(CanvasMain);
            _game = new GameGraph(_engine, null);
            _game.ReDraw();
        }

        private void ButtonAddCity_Click(object sender, RoutedEventArgs e)
        {
            WindowCityCreate window = new WindowCityCreate(_game, null);
            window.ShowDialog();

            _game.ReDraw();
        }

        private void ButtonCreateCity_Click(object sender, RoutedEventArgs e)
        {
            WindowChooseGameObject windowChoose = new WindowChooseGameObject(new List<GameObject>(_game.GetCitys()));
            windowChoose.ShowDialog();

            if (windowChoose.GetSelectedGameObject() != null)
            {
                City city = (City)windowChoose.GetSelectedGameObject();
                WindowCityCreate windowCity = new WindowCityCreate(_game, city);
                windowCity.ShowDialog();

                _game.ReDraw();
            }
        }

        private void ButtonAddRoad_Click(object sender, RoutedEventArgs e)
        {
            WindowRoadCreate window = new WindowRoadCreate(_game, null);
            window.ShowDialog();

            _game.ReDraw();
        }

        private void ButtonCreateRoad_Click(object sender, RoutedEventArgs e)
        {
            WindowChooseGameObject windowChoose = new WindowChooseGameObject(new List<GameObject>(_game.GetRoads()));
            windowChoose.ShowDialog();

            if (windowChoose.GetSelectedGameObject() != null)
            {
                GraphRoad road = (GraphRoad)windowChoose.GetSelectedGameObject();
                WindowRoadCreate windowRoad = new WindowRoadCreate(_game, road);
                windowRoad.ShowDialog();

                _game.ReDraw();
            }
        }

        private void ButtonCreateTransport_Click(object sender, RoutedEventArgs e)
        {
            Transport transport = _game.GetTransports().First();
            WindowTransportCreate window = new WindowTransportCreate(_game, transport);

            window.ShowDialog();

            _game.ReDraw();
        }

        private void ButtonDeleteRoad_Click(object sender, RoutedEventArgs e)
        {
            WindowChooseGameObject windowChoose = new WindowChooseGameObject(new List<GameObject>(_game.GetRoads()));
            windowChoose.ShowDialog();

            if (windowChoose.GetSelectedGameObject() != null)
            {
                GraphRoad road = (GraphRoad)windowChoose.GetSelectedGameObject();
                _game.DeleteRoad(road);

                _game.ReDraw();
            }
        }

        private void ButtonDeleteCity_Click(object sender, RoutedEventArgs e)
        {
            WindowChooseGameObject windowChoose = new WindowChooseGameObject(new List<GameObject>(_game.GetCitys()));
            windowChoose.ShowDialog();

            if (windowChoose.GetSelectedGameObject() != null)
            {
                City city = (City)windowChoose.GetSelectedGameObject();
                _game.DeleteCity(city);
                _game.ReDraw();
            }
        }

        private void ButtonSaveLevelJson_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"json files(*.json) | *.json";

            if(saveFileDialog.ShowDialog() == true)
            {
                GameGraphJSON gameJSON = StandardGraphGame.JSON.Parsing.ParsingGameGraphToGameGraphJSON(_game);
                string json = JsonConvert.SerializeObject(gameJSON);

                File.Delete(saveFileDialog.FileName);
                using (StreamWriter file = new StreamWriter(saveFileDialog.FileName))
                {
                    file.Write(json);
                }
            }
        }

        private void ButtonLoadLevelJson_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"json files(*.json) | *.json";

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader file = new StreamReader(openFileDialog.FileName))
                {
                    string json = file.ReadToEnd();
                    GameGraphJSON gameJSON = JsonConvert.DeserializeObject<GameGraphJSON>(json);
                    _game = StandardGraphGame.JSON.Parsing.ParsingGameGraphJSONToGameGraph(gameJSON, _engine);

                    _game.StartLevel();
                    _game.ReDraw();
                }

            }
        }
    }
}
